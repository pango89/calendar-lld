namespace Calendar
{
    public class User
    {
        private static int counter = 0;

        public User(string email, TimeSlot workingHours)
        {
            Id = ++counter;
            Email = email;
            WorkingHours = workingHours;
            Events = new();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public TimeSlot WorkingHours { get; set; }
        public Team? Team { get; set; }
        public List<Event> Events { get; set; }

        public void SetTeam(Team team)
        {
            if (this.Team != null)
                throw new TeamReassignmentException();

            Team = team;
        }

        public void AddEvent(Event meeting)
        {
            this.Events.Add(meeting);
        }

        public void Print()
        {
            foreach (var e in Events)
            {
                Console.WriteLine("{0} - {1} - {2} - {3}", Email, e.Title, e.TimeSlot.Start, e.TimeSlot.End);
            }
        }
    }
}