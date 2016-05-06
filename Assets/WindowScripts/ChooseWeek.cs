using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Windows;

namespace CoreSys
{
    public class ChooseWeek : MonoBehaviour
    {
        public ScheduleWindow parent;
        public GameObject weekBarParent;
        public ToggleGroup toggleGroup;
        public PrefabList prefabs;
        private int weekDistance = 4;//This is how many weeks forward it will check/generate
        public DateTime date;
        public List<Week> weekList = new List<Week>();
        public List<Toggle> toggleList = new List<Toggle>();

        /// <summary>
        /// This method will using the current date and find all weeks moving forward within a limited timeframe.
        /// </summary>
        public void FindWeeks()
        {
            date = DateTime.Now.Date;
            for (int i = 0; i < weekDistance; i++)
            {
                int daysToFirstWeek = DayOfWeekValue(date.DayOfWeek);
                date = date.AddDays(daysToFirstWeek);
                Week tempWeek = CoreSystem.FindWeek(date);
                if (tempWeek == null)//Week has not been created, generate
                {
                    tempWeek = new Week(date);
                    CoreSystem.weekList.Add(date, tempWeek);
                    weekList.Add(tempWeek);
                }
                else//Week has been generated, and we will replace it if chosen
                {
                    tempWeek = new Week(date);
                    weekList.Add(tempWeek);
                }
            }

            for (int i = 0; i < weekList.Count; i++)
            {
                GameObject temp = WindowInstantiator.SpawnWindow(prefabs.prefabList[0], weekBarParent);
                WeekBar tempWeekBar = temp.GetComponent<WeekBar>();
                Toggle tempToggle = temp.GetComponent<Toggle>();
                tempToggle.group = toggleGroup;
                toggleList.Add(tempToggle);
                string weekText = ("Week of " + weekList[i].startDate.ToShortDateString());
                tempWeekBar.SetBar(this, i, weekText);
            }
        }

        public void Submit()
        {
            for (int i = 0; i < toggleList.Count; i++)
            {
                if (toggleList[i].isOn)
                {
                    WeekSelected(i);
                    return;
                }
            }
        }

        private void WeekSelected(int index)
        {
            parent.WeeklyConfig(weekList[index]);
        }

        //This method converts the current day of the week into a value needed to get to the start of the next week
        private int DayOfWeekValue(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return 7;
                case DayOfWeek.Monday:
                    return 6;
                case DayOfWeek.Tuesday:
                    return 5;
                case DayOfWeek.Wednesday:
                    return 4;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 2;
                case DayOfWeek.Saturday:
                    return 1;
                default:
                    Debug.Log("Default case thrown in switch || ChooseWeek.cs || DayOfWeekValue()");
                    return -1;
            }
        }
    }
}