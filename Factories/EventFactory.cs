namespace Calendar
{
    public class EventFactory
    {
        public static Event Create(string title, DateTime start, DateTime end, List<User> users)
        {
            return new Event(title, new TimeSlot(start, end), users);
        }
    }
}