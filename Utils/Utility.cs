namespace Calendar
{
    public class Utility
    {
        /// <summary>
        /// Given a List of Occupied Time Slots i.e. Start and End Times
        /// Return if there is any free slot available between Given Start and End Time
        /// Also Return the slots if available
        /// </summary>
        /// <param name="timeSlots"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static (List<TimeSlot>, bool) GetAvailableSlots(List<TimeSlot> timeSlots, DateTime start, DateTime end)
        {
            timeSlots.Sort((x, y) => x.Start.CompareTo(y.Start));

            List<TimeSlot> availableSlots = new();

            DateTime currentStart = start;

            foreach (var ts in timeSlots)
            {
                if (currentStart < ts.Start)
                {
                    availableSlots.Add(new TimeSlot(currentStart, ts.Start));
                }

                currentStart = ts.End;
            }

            if (currentStart < end)
                availableSlots.Add(new TimeSlot(currentStart, end));

            return (availableSlots, availableSlots.Count > 0);
        }

        public static List<TimeSlot> MergeTimeSlots(List<TimeSlot> timeSlots)
        {
            timeSlots.Sort((x, y) => x.Start.CompareTo(y.Start));
            List<TimeSlot> mergedSlots = new();

            var prev = timeSlots[0];

            for (int i = 1; i < timeSlots.Count; i++)
            {
                TimeSlot curr = timeSlots[i];

                if (curr.Start > prev.End)
                {
                    mergedSlots.Add(prev);
                    prev = curr;
                    continue;
                }

                if (curr.End > prev.End)
                    prev.End = curr.End;
            }

            return mergedSlots;
        }
    }
}