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
        public SerializableDictionary<int, int> openNeededShifts = new SerializableDictionary<int,int>();
        public SerializableDictionary<int, int> closeNeededShifts = new SerializableDictionary<int,int>();
        public SerializableDictionary<int, int> openScheduledShifts = new SerializableDictionary<int, int>();
        public SerializableDictionary<int, int> closeScheduledShifts = new SerializableDictionary<int, int>();
        
        public SerializableDictionary<EmployeeScheduleWrapper, Shift> shiftDictionary = new SerializableDictionary<EmployeeScheduleWrapper, Shift>();
        //This was changed from being a list to a single value since you cannot have more than one shift in a day.

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

        public void SetOpenNeeded(int key, int value)
        {
            try
            {
                openNeededShifts.Add(key, value);
            }
            catch
            {
                Debug.Log(key + " already existed in OpenNeeded dictionary");
                openNeededShifts.Remove(key);
                openNeededShifts.Add(key, value);
            }
        }

        public void SetCloseNeeded(int key, int value)
        {
            try
            {
                closeNeededShifts.Add(key, value);
            }
            catch
            {
                Debug.Log(key + " already existed in CloseNeeded dictionary");
                closeNeededShifts.Remove(key);
                closeNeededShifts.Add(key, value);
            }
        }
    }
}
