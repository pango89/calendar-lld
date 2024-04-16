namespace Calendar
{
    public class UserFactory
    {
        public static User Create(string email, DateTime start, DateTime end)
        {
            return new User(email, new TimeSlot(start, end));
        }
    }
}