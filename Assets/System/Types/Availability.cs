using UnityEngine;
using System.Collections;
using CoreSys.Employees;

namespace CoreSys
{
    public class Availability
    {
        public Day sunday, monday, tuesday, wednesday, thursday, friday, saturday;

        public Availability() { }
        public Availability(Availability copy)
        {
            sunday = new Day(copy.sunday);
            monday = new Day(copy.monday);
            tuesday = new Day(copy.tuesday);
            wednesday = new Day(copy.wednesday);
            thursday = new Day(copy.thursday);
            friday = new Day(copy.friday);
            saturday = new Day(copy.saturday);
        }
        
        public Day this[DayOfWeek d]
        {
            switch(d)
            {
                case DayOfWeek.Sunday:
                    return sunday;
                case DayOfWeek.Monday:
                    return monday;
                case DayOfWeek.Tuesday:
                    return tuesday;
                case DayOfWeek.Wednesday:
                    return wednesday;
                case DayOfWeek.Thursday:
                    return thursday;
                case DayOfWeek.Friday:
                    return friday;
                case DayOfWeek.Saturday:
                    return saturday;
            }
        }
    }
}
