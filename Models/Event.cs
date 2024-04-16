namespace Calendar
{
    public class Event
    {
        private static int counter = 0;

        public Event(string title, TimeSlot timeSlot, List<User> users)
        {
            Id = ++counter;
            Title = title;
            TimeSlot = timeSlot;
            Participants = new(users);
            AcceptanceStatus = new();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public HashSet<User> Participants { get; set; }
        public Dictionary<int, InvitationAcceptanceStatus> AcceptanceStatus { get; set; }

        public void AddParticipants(User user)
        {
            this.Participants.Add(user);
            if (!this.AcceptanceStatus.ContainsKey(user.Id))
                this.AcceptanceStatus.Add(user.Id, InvitationAcceptanceStatus.NO_ACTION);
        }
    }
}