namespace Calendar
{
    public class EventService
    {
        private readonly UserService userService;
        private readonly TeamService teamService;

        private readonly Dictionary<int, Event> events; // This can be moved to its own Repository

        public EventService(UserService userService, TeamService teamService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.events = new();
        }

        public Event CreateEvent(string title, DateTime start, DateTime end, List<int> userIds, List<int> teamIds, int representation)
        {
            List<User> eventUsers = new();
            foreach (var userId in userIds)
            {
                var user = userService.GetUserById(userId);
                if (!this.userService.IsWithinWorkingHours(user, start, end))
                    throw new UserNotFreeException();

                var (slots, isFree) = GetAvailableSlotsForUser(user, start, end);

                if (!isFree)
                    throw new UserNotFreeException();

                eventUsers.Add(user);
            }

            foreach (var teamId in teamIds)
            {
                var team = teamService.GetTeamById(teamId);
                int repsFound = 0;

                foreach (var user in team.Users)
                {
                    if (!this.userService.IsWithinWorkingHours(user, start, end))
                        continue;

                    var (slots, isFree) = GetAvailableSlotsForUser(user, start, end);

                    if (isFree)
                    {
                        ++repsFound;
                        eventUsers.Add(user);
                    }

                    if (repsFound == representation)
                        break;
                }

                if (repsFound < representation)
                    throw new Exception("Not possible");
            }

            Event meeting = EventFactory.Create(title, start, end, eventUsers);
            events.Add(meeting.Id, meeting);
            foreach (var user in eventUsers)
            {
                user.AddEvent(meeting);
            }

            return meeting;
        }

        public static (List<TimeSlot>, bool) GetAvailableSlotsForUser(User user, DateTime start, DateTime end)
        {
            var occupiedTimeSlots = user.Events.Select(x => x.TimeSlot).ToList();

            var dateOnly = DateOnly.FromDateTime(start);
            // var dateOnly = DateOnly.FromDateTime(end); Assume Same as start date

            var x1 = TimeOnly.FromDateTime(user.WorkingHours.Start);
            var y1 = TimeOnly.FromDateTime(user.WorkingHours.End);

            occupiedTimeSlots.Add(new TimeSlot(start, dateOnly.ToDateTime(x1)));
            occupiedTimeSlots.Add(new TimeSlot(dateOnly.ToDateTime(y1), end));

            return Utility.GetAvailableSlots(occupiedTimeSlots, start, end);
        }

        public List<TimeSlot> GetCommonAvailableSlotsForUsers(List<int> userIds, List<int> teamIds, DateTime start, DateTime end, int representation = 1)
        {
            HashSet<int> mergedUserIds = new();
            // mergedUserIds.UnionWith(userIds);
            teamIds.ForEach(x => mergedUserIds.UnionWith(teamService.GetTeamById(x).Users.Select(y => y.Id)));

            var slots = mergedUserIds.SelectMany(x => GetAvailableSlotsForUser(this.userService.GetUserById(x), start, end).Item1).ToList();
            return Utility.MergeTimeSlots(slots);
        }

        // public static (List<TimeSlot>, bool) GetCommonAvailableSlotsForUsers(List<User> users, DateTime start, DateTime end)
        // {
        //     var timeSlots = users.SelectMany(x => x.Events.Select(y => y.TimeSlot)).ToList();
        //     return Utility.GetAvailableSlots(timeSlots, start, end);
        // }

        public static List<Event> GetEventsOfUserBetweenTime(User user, DateTime start, DateTime end)
        {
            // user.Events.Sort((x, y) => x.TimeSlot.Start.CompareTo(y.TimeSlot.Start));
            List<Event> meetings = new();

            foreach (var meeting in user.Events)
            {
                TimeSlot ts = meeting.TimeSlot;

                if (ts.Start < start && ts.End < start)
                {
                    continue;
                }

                if (ts.Start > end && ts.End > end)
                {
                    continue;
                }

                meetings.Add(meeting);
            }

            return meetings;
        }

        public void CreateRecurringEvent(
            string title,
            DateOnly startDate,
            DateOnly endDate,
            TimeOnly meetingStartTime,
            TimeOnly meetingEndTime,
            Frequency frequency,
            List<int> userIds,
            List<int> teamIds,
            int representation)
        {
            var curr = startDate;

            while (curr <= endDate)
            {
                DateTime meetingStartDateTime = curr.ToDateTime(meetingStartTime);
                DateTime meetingEndDateTime = curr.ToDateTime(meetingEndTime);

                var e = CreateEvent(title, meetingStartDateTime, meetingEndDateTime, userIds, teamIds, representation);

                if (frequency == Frequency.DAILY)
                    curr = curr.AddDays(1);
                else if (frequency == Frequency.WEEKLY)
                    curr = curr.AddDays(7);
                else if (frequency == Frequency.MONTHLY)
                    curr = curr.AddDays(30);
            }

        }
    }
}