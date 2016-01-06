using UnityEngine;
using System.Collections;
using CoreSys.Employees;

namespace CoreSys
{
    public class ScheduledEmployee
    {
        public Employee employee;
        //startShift and endShift will be used as the primaries, if there is a split these will still be the main shift.
        public int startShift, endShift, shiftLength;
        public bool splitShift = false;
        //used for initial split shift if there is one
        public int startSplit, endSplit;
    }
}