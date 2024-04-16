namespace Calendar
{
    public class TeamFactory
    {
        public static Team Create(string name)
        {
            return new Team(name);
        }
    }
}