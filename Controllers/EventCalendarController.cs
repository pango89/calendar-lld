namespace Calendar
{
    class EventCalendarController
    {
        private readonly UserService userService;
        private readonly TeamService teamService;
        private readonly EventService eventService;

        public EventCalendarController()
        {
            this.userService = new UserService();
            this.teamService = new TeamService(userService);
            this.eventService = new EventService(userService, teamService);
        }

        public User CreateUser(string email, DateTime start, DateTime end)
        {
            return userService.AddUser(email, start, end);
        }

        public Team CreateTeam(string name, List<int> userIds)
        {
            return teamService.CreateTeam(name, userIds);
        }

        public Event CreateEvent(string title, DateTime start, DateTime end, List<int> userIds, List<int> teamIds, int representation)
        {
            return eventService.CreateEvent(title, start, end, userIds, teamIds, representation);
        }

        public void CreateRecurringEvent(string title,
            DateOnly startDate,
            DateOnly endDate,
            TimeOnly meetingStartTime,
            TimeOnly meetingEndTime,
            Frequency frequency,
            List<int> userIds,
            List<int> teamIds,
            int representation)
        {
            eventService.CreateRecurringEvent(title, startDate, endDate, meetingStartTime, meetingEndTime, frequency, userIds, teamIds, representation);
        }

        public List<Event> GetEventsOfUserBetweenTime(int userId, DateTime start, DateTime end)
        {
            return EventService.GetEventsOfUserBetweenTime(this.userService.GetUserById(userId), start, end);
        }

        public List<TimeSlot> GetCommonAvailableSlotsForUsers(List<int> userIds, List<int> teamIds, DateTime start, DateTime end, int representation = 1)
        {
            return eventService.GetCommonAvailableSlotsForUsers(userIds, teamIds, start, end);
        }

    }
}