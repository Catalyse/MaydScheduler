using UnityEngine;
using System.Collections;

namespace CoreSys
{
    public class Day
    {
        public bool available;
        public int startTime, endTime;
        private int defaultStart = 0800;
        private int defaultEnd = 2200;

        /// <summary>
        /// If Day is instantiated using its default constructor it is assumed to be not available or false.
        /// </summary>
        public Day()
        {
            available = false;
            startTime = 0;
            endTime = 0;
        }
        /// <summary>
        /// If day is instantiated without start and end times it is assumed to be the default times of 08-22
        /// </summary>
        /// <param name="on"></param>
        public Day(bool on)
        {
            available = on;
            if (available)
            {
                startTime = defaultStart;
                endTime = defaultEnd;
            }
            else
            {
                startTime = 0;
                endTime = 0;
            }
        }
        public Day(bool on, int s, int e)
        {
            available = on;
            startTime = s;
            endTime = e;
        }

        //The true false return operators are overloaded so that this type can be treated like a bool while still holding information
    }
}