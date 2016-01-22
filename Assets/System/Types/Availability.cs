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
    }
}