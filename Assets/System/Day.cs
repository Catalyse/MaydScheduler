using UnityEngine;
using System.Collections;

namespace CoreSys.Types
{
    public class Day
    {
        public bool available;
        public int startTime, endTime;
        private int defaultStart = 1000;

        public Day() { }
        public Day(bool on)
        {
            available = on;
        }
        public Day(bool on, int s, int e)
        {
            available = on;
            startTime = s;
            endTime = e;
        }

        //The true false return operators are overloaded so that this type can be treated like a bool while still holding information
        public static bool operator true(Day d) { return d.available; }
        public static bool operator false(Day d) { return d.available; }
    }
}