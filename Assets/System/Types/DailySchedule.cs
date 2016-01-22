using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public class DailySchedule
    {
        public int dayOfYear;
        public List<EmployeeScheduleWrapper> employeeList = new List<EmployeeScheduleWrapper>();
        //Dictionary<(Hour), List<(scheduled shifts)>>
        public Dictionary<int, List<Shift>> schedule = new Dictionary<int, List<Shift>>();

        public DailySchedule()
        {
            Debug.Log("DailySchedule Initialized without a date! || DailySchedule.cs || Default CTOR");
        }

        public DailySchedule(int day)
        {
            dayOfYear = day;
        }
    }
}