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
            sunday = copy.sunday;
            monday = copy.monday;
            tuesday = copy.tuesday;
            wednesday = copy.wednesday;
            thursday = copy.thursday;
            friday = copy.friday;
            saturday = copy.saturday;
        }
    }
}