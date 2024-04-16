namespace Calendar
{
    public class TeamService
    {
        private readonly TeamRepository teamRepository;
        private readonly UserService userService;

        public TeamService(UserService userService)
        {
            this.teamRepository = new TeamRepository();
            this.userService = userService;
        }

        public Team GetTeamById(int id) => this.teamRepository.GetTeamById(id);
        public Team CreateTeam(string name, List<int> userIds)
        {
            this.AreValidUsers(userIds);
            var team = TeamFactory.Create(name);

            foreach (var userId in userIds)
            {
                var user = this.userService.GetUserById(userId);
                user.SetTeam(team);
                team.AddMember(user);
            }

            teamRepository.AddTeam(team);
            return team;
        }

        private bool AreValidUsers(List<int> userIds)
        {
            foreach (var userId in userIds)
            {
                if (userService.IsPartOfAnyTeam(userId))
                {
                    throw new TeamReassignmentException();
                }
            }

            return true;
        }
    }
}