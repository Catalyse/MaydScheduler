using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CoreSys;

namespace CoreSys.Windows
{
    public class ScheduleWindow : Window
    {
        public Text title;
        public Week currentWeek;
        public GameObject grid, dailyStaffing, weeklyConfig, chooseWeek;
        public PrefabList prefabs;

        public ScheduleWindow() { }

        public void StartSchedule()
        {
            chooseWeek.SetActive(true);
            chooseWeek.GetComponent<ChooseWeek>().FindWeeks();//Find weeks to schedule
        }

        public void WeeklyConfig(Week week)
        {
            chooseWeek.SetActive(false);
            currentWeek = week;
            weeklyConfig.SetActive(true);
        }

        public void DailyStaffing()
        {
            weeklyConfig.SetActive(false);
            dailyStaffing.SetActive(true);
        }

        public void GenerateSchedule()
        {
            dailyStaffing.SetActive(false);
            SchedulingAlgorithm.GenerateSchedule(currentWeek);
        }

        public void DrawSchedule(Week schedule)
        {
            for (int i = 0; i < schedule.empList.Count; i++)
            {
                GameObject newWindow = WindowInstantiator.SpawnWindow(prefabs.prefabList[0], grid);
                EmployeeScheduleBar bar = newWindow.GetComponent<EmployeeScheduleBar>();
                bar.SetBar(schedule.empList[i]);
            }
        }
    }
}