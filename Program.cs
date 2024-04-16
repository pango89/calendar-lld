using Calendar;

internal class Program
{
    private static void Main(string[] args)
    {
        EventCalendarController evc = new();
        var userA = evc.CreateUser("A", new DateTime(2000, 1, 1, 10, 0, 0), new DateTime(2000, 1, 1, 19, 0, 0));
        var userB = evc.CreateUser("B", new DateTime(2000, 1, 1, 09, 30, 0), new DateTime(2000, 1, 1, 17, 30, 0));
        var userC = evc.CreateUser("C", new DateTime(2000, 1, 1, 11, 30, 0), new DateTime(2000, 1, 1, 18, 30, 0));
        var userD = evc.CreateUser("D", new DateTime(2000, 1, 1, 10, 00, 0), new DateTime(2000, 1, 1, 18, 00, 0));
        var userE = evc.CreateUser("E", new DateTime(2000, 1, 1, 11, 00, 0), new DateTime(2000, 1, 1, 19, 30, 0));
        var userF = evc.CreateUser("F", new DateTime(2000, 1, 1, 11, 00, 0), new DateTime(2000, 1, 1, 18, 30, 0));

        var team1 = evc.CreateTeam("T1", new List<int> { userC.Id, userE.Id });
        var team2 = evc.CreateTeam("T2", new List<int> { userB.Id, userD.Id, userF.Id });

        var e1 = evc.CreateEvent("E1", new DateTime(2023, 10, 24, 14, 0, 0), new DateTime(2023, 10, 24, 15, 0, 0), new List<int> { userA.Id }, new List<int> { team1.Id }, 2);
        // var e2 = evc.CreateEvent("E2", new DateTime(2023, 10, 24, 14, 0, 0), new DateTime(2023, 10, 24, 15, 0, 0), new List<int> { userC.Id }, new List<int> { }, 0);
        var e3 = evc.CreateEvent("E3", new DateTime(2023, 10, 23, 15, 0, 0), new DateTime(2023, 10, 23, 16, 0, 0), new List<int> { }, new List<int> { team1.Id, team2.Id }, 2);
        var e4 = evc.CreateEvent("E4", new DateTime(2023, 10, 23, 15, 0, 0), new DateTime(2023, 10, 23, 16, 0, 0), new List<int> { userA.Id }, new List<int> { team2.Id }, 1);
        // var e5 = evc.CreateEvent("E5", new DateTime(2023, 10, 23, 10, 0, 0), new DateTime(2023, 10, 23, 11, 0, 0), new List<int> { userA.Id, userF.Id }, new List<int> { }, 0);

        var eventsA = evc.GetEventsOfUserBetweenTime(userA.Id, new DateTime(2023, 10, 23, 10, 0, 0), new DateTime(2023, 10, 24, 17, 0, 0));
        var eventsB = evc.GetEventsOfUserBetweenTime(userB.Id, new DateTime(2023, 10, 23, 10, 0, 0), new DateTime(2023, 10, 24, 17, 0, 0));

        var slots = evc.GetCommonAvailableSlotsForUsers(new List<int> { userA.Id }, new List<int> { team1.Id }, new DateTime(2023, 10, 23, 0, 0, 0), new DateTime(2023, 10, 23, 23, 59, 0));

        evc.CreateRecurringEvent(
            "RE1",
            new DateOnly(2023, 11, 1),
            new DateOnly(2023, 11, 30),
            new TimeOnly(14, 0, 0),
            new TimeOnly(15, 0, 0),
            Frequency.DAILY,
            new List<int> { userA.Id },
            new List<int> { team1.Id },
            2
        );

        Console.WriteLine();
    }
}