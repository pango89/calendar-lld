namespace Calendar
{
    public class Team
    {
        private static int counter = 0;

        public Team(string name)
        {
            Id = ++counter;
            Users = new();
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<User> Users { get; set; }

        public void AddMember(User user)
        {
            this.Users.Add(user);
        }

        public void RemoveMember(User user)
        {
            this.Users.Remove(user);
        }
    }
}