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
        public SerializableDictionary<int, int> openShifts = new SerializableDictionary<int,int>();
        public SerializableDictionary<int, int> closeShifts = new SerializableDictionary<int,int>();
        public SerializableDictionary<int, int> openScheduled = new SerializableDictionary<int, int>();
        public SerializableDictionary<int, int> closeScheduled = new SerializableDictionary<int, int>();
        //Dictionary<(position), List<(Employees)>>
        public SerializableDictionary<int, List<EmployeeScheduleWrapper>> employeeList = new SerializableDictionary<int, List<EmployeeScheduleWrapper>>();
        //Dictionary<(Hour), List<(scheduled shifts)>>
        //This was redesigned for open/close specific shifting, not meant to handle hourly anymore. However it should be redesigned to be more flexible in the future.
        //public Dictionary<int, List<Shift>> openShifts = new Dictionary<int, List<Shift>>(), closeShifts = new Dictionary<int, List<Shift>>();
        public SerializableDictionary<EmployeeScheduleWrapper, List<Shift>> shiftDictionary = new SerializableDictionary<EmployeeScheduleWrapper, List<Shift>>();

        public DailySchedule() { }

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
