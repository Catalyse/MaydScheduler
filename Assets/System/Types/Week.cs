using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;

namespace CoreSys
{
    public class Week
    {
        public int julianStartDay;
        public DateTime startDate;
        public int earliestStart, latestEnd;
        public List<EmployeeScheduleWrapper> empList;//Including temp avail, as well as shifts(which can also be found daily)
        public DailySchedule sunday, monday, tuesday, wednesday, thursday, friday, saturday;

        /// <summary>
        /// Week should not be initialized with the default constructor || This exists for serialization
        /// </summary>
        public Week() { }

        /// <summary>
        /// Depreciated Constructor type
        /// </summary>
        /// <param name="startDay"></param>
        public Week(int startDay)
        {
            julianStartDay = startDay;
            Init();
        }

        /// <summary>
        /// Week is generated with a DateTime start date
        /// </summary>
        /// <param name="startDay"></param>
        /// TODO // Add check to make sure the start date is a sunday
        public Week(DateTime startDay)
        {
            startDate = startDay.Date;
            julianStartDay = startDay.DayOfYear;
            Init();
        }

        public DailySchedule SelectDay(int i)
        {
            switch (i)
            {
                case 0:
                    return sunday;
                case 1:
                    return monday;
                case 2:
                    return tuesday;
                case 3:
                    return wednesday;
                case 4:
                    return thursday;
                case 5:
                    return friday;
                case 6:
                    return saturday;
                default:
                    Debug.Log("Default case chosen! || Week.cs || SelectDay");
                    return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="openNeeds"> Dictionary<Day, Need> </param>
        /// <param name="closeNeeds"> Dictionary<Day, Need> </param>
        public void SetNeeds(int position, SerializableDictionary<int, int> openNeeds, SerializableDictionary<int, int> closeNeeds)
        {
            sunday.SetOpenNeeded(position, openNeeds[0]);
            monday.SetOpenNeeded(position, openNeeds[1]);
            tuesday.SetOpenNeeded(position, openNeeds[2]);
            wednesday.SetOpenNeeded(position, openNeeds[3]);
            thursday.SetOpenNeeded(position, openNeeds[4]);
            friday.SetOpenNeeded(position, openNeeds[5]);
            saturday.SetOpenNeeded(position, openNeeds[6]);
            sunday.SetCloseNeeded(position, closeNeeds[0]);
            monday.SetCloseNeeded(position, closeNeeds[1]);
            tuesday.SetCloseNeeded(position, closeNeeds[2]);
            wednesday.SetCloseNeeded(position, closeNeeds[3]);
            thursday.SetCloseNeeded(position, closeNeeds[4]);
            friday.SetCloseNeeded(position, closeNeeds[5]);
            saturday.SetCloseNeeded(position, closeNeeds[6]);
        }

        public void FillWeekDays(Dictionary<int, DailySchedule> days)
        {
            //idk wtf this is
        }

        //Set the basics of the week on a day to day basis.
        public void SetWeek(int suStartHour, int mStartHour, int tuStartHour, int wStartHour, int thStartHour, int fStartHour, int saStartHour, int suEndHour, int mEndHour,
            int tuEndHour, int wEndHour, int thEndHour, int fEndHour, int saEndHour, bool sun, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat)
        {
            sunday.SetBaseInfo(sun, suStartHour, suEndHour);
            monday.SetBaseInfo(mon, mStartHour, mEndHour);
            tuesday.SetBaseInfo(tue, tuStartHour, tuEndHour);
            wednesday.SetBaseInfo(wed, wStartHour, wEndHour);
            thursday.SetBaseInfo(thu, thStartHour, thEndHour);
            friday.SetBaseInfo(fri, fStartHour, fEndHour);
            saturday.SetBaseInfo(sat, saStartHour, saEndHour);
            //End bool set
        }

        private void Init()
        {
            sunday = new DailySchedule(startDate);
            monday = new DailySchedule(startDate.AddDays(1));
            tuesday = new DailySchedule(startDate.AddDays(2));
            wednesday = new DailySchedule(startDate.AddDays(3));
            thursday = new DailySchedule(startDate.AddDays(4));
            friday = new DailySchedule(startDate.AddDays(5));
            saturday = new DailySchedule(startDate.AddDays(6));
            empList = new List<EmployeeScheduleWrapper>();
        }
    }
}