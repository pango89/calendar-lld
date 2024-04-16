namespace Calendar
{
    public class UserRepository
    {
        private readonly Dictionary<int, User> users;

        public UserRepository()
        {
            users = new();
        }

        public User GetUserById(int userId)
        {
            if (!users.ContainsKey(userId))
                throw new UserNotFoundException();

            return users[userId];
        }

        public void AddUser(User user)
        {
            if (users.ContainsKey(user.Id))
                throw new DuplicateUserException();

            users.Add(user.Id, user);
        }

        public void SetTeam(int id, Team team)
        {
            if (!users.ContainsKey(id))
                throw new UserNotFoundException();

            users[id].SetTeam(team);
        }

        public void AddEvent(int id, Event meeting)
        {
            if (!users.ContainsKey(id))
                throw new UserNotFoundException();

            users[id].AddEvent(meeting);
        }
    }
}