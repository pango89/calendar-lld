namespace Calendar
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService()
        {
            this.userRepository = new UserRepository();
        }

        public User GetUserById(int id) => this.userRepository.GetUserById(id);
        public bool IsPartOfAnyTeam(int userId) => this.userRepository.GetUserById(userId).Team != null;
        public User AddUser(string email, DateTime start, DateTime end)
        {
            var user = UserFactory.Create(email, start, end);
            this.userRepository.AddUser(user);
            return user;
        }
        public void SetTeam(User user, Team team) => this.userRepository.AddUser(user);
        public bool IsWithinWorkingHours(User user, DateTime start, DateTime end)
        {
            var s1 = TimeOnly.FromDateTime(user.WorkingHours.Start);
            var s2 = TimeOnly.FromDateTime(start);
            var s3 = TimeOnly.FromDateTime(end);
            var s4 = TimeOnly.FromDateTime(user.WorkingHours.End);

            return s2 >= s1 && s3 <= s4;
        }
    }
}