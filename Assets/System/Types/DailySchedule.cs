using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public class DailySchedule
    {
        public DateTime date;
        public bool activeDay;
        public int openTime, closeTime;
        public DayOfWeek dayOfWeek;
        public Dictionary<int, int> openShifts, closeShifts;
        //Dictionary<(position), List<(Employees)>>
        public Dictionary<int, List<EmployeeScheduleWrapper>> employeeList = new Dictionary<int, List<EmployeeScheduleWrapper>>();
        //Dictionary<(Hour), List<(scheduled shifts)>>
        //This was redesigned for open/close specific shifting, not meant to handle hourly anymore. However it should be redesigned to be more flexible in the future.
        public Dictionary<int, List<Shift>> openShifts = new Dictionary<int, List<Shift>>(), closeShifts = new Dictionary<int, List<Shift>>();

        public DailySchedule()
        {
            Debug.Log("DailySchedule Initialized without a date! || DailySchedule.cs || Default CTOR");
        }

        public DailySchedule(DateTime day)
        {
            date = day;
            dayOfWeek = day.DayOfWeek;
        }

        public void SetBaseInfo(bool active)
        {
            activeDay = active;
            openTime = 0;
            closeTime = 0;
        }

        public void SetBaseInfo(bool active, int open, int close)
        {
            activeDay = active;
            openTime = open;
            closeTime = close;
        }
    }
}
