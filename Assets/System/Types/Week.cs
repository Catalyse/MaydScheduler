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
        public int weekID;
        public int julianStartDay;
        public DateTime startDate;
        public int earliestStart, latestEnd;
        public List<EmployeeScheduleWrapper> empList;//Including temp avail, as well as shifts(which can also be found daily)
        public DailySchedule sunday, monday, tuesday, wednesday, thursday, friday, saturday;
        //Position type(refer to CoreSystem for definition)
        //Dictionary<(Position), Dictionary<Day, (need(int))>>
        public Dictionary<int, Dictionary<int, int>> openNeeds = new Dictionary<int, Dictionary<int, int>>();
        public Dictionary<int, Dictionary<int, int>> closeNeeds = new Dictionary<int, Dictionary<int, int>>();
        // Use this for initialization

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
            weekID = CoreSystem.GenerateWeekID();
        }

        /// <summary>
        /// Week is generated with a DateTime start date
        /// </summary>
        /// <param name="startDay"></param>
        /// TODO // Add check to make sure the start date is a sunday
        public Week(DateTime startDay)
        {
            startDate = startDay;
            julianStartDay = startDay.DayOfYear;
            weekID = CoreSystem.GenerateWeekID();
        }

        public DailySchedule SelectDay(int i)
        {
            switch (i)
            {
                case 1:
                    return sunday;
                case 2:
                    return monday;
                case 3:
                    return tuesday;
                case 4:
                    return wednesday;
                case 5:
                    return thursday;
                case 6:
                    return friday;
                case 7:
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
        public void SetNeeds(int position, Dictionary<int, int> openNeeds, Dictionary<int, int> closeNeeds)
        {
            this.openNeeds[position] = openNeeds;
            this.closeNeeds[position] = closeNeeds;
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
    }
}