using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public class DailySchedule
    {
        public int dayOfYear;
        public int openTime, closeTime;
        //Dictionary<(position), List<(Employees)>>
        public Dictionary<int, List<EmployeeScheduleWrapper>> employeeList = new Dictionary<int, List<EmployeeScheduleWrapper>>();
        //Dictionary<(Hour), List<(scheduled shifts)>>
        public Dictionary<int, Dictionary<int, List<Shift>>> schedule = new Dictionary<int, Dictionary<int, List<Shift>>>();

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