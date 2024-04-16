namespace Calendar
{
    public class TeamRepository
    {
        private readonly Dictionary<int, Team> teams;

        public TeamRepository()
        {
            teams = new();
        }

        public Team GetTeamById(int teamId)
        {
            if (!teams.ContainsKey(teamId))
                throw new TeamNotFoundException();

            return teams[teamId];
        }

        public void AddTeam(Team team)
        {
            if (teams.ContainsKey(team.Id))
                throw new DuplicateTeamException();

            teams.Add(team.Id, team);
        }
    }
}