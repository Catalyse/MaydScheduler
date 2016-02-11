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
                case Sunday:
                    return sunday;
                case Monday:
                    return monday;
                case Tuesday:
                    return tuesday;
                case Wednesday:
                    return wednesday;
                case Thursday:
                    return thursday;
                case Friday:
                    return friday;
                case Saturday:
                    return saturday;
            }
        }
    }
}
