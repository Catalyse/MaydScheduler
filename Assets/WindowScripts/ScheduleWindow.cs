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
            chooseWeek.GetComponent<ChooseWeek>().FindWeeks();
        }

        public void WeeklyConfig(Week week)
        {
            currentWeek = week;
            weeklyConfig.SetActive(true);
            chooseWeek.SetActive(false);
        }

        public void DailyStaffing()
        {
            weeklyConfig.SetActive(false);
            dailyStaffing.SetActive(true);
        }

        public void GenerateSchedule()
        {
            SchedulingAlgorithm.GenerateSchedule(currentWeek);
        }

        private void SetupWeek()
        {
            dailyStaffing.gameObject.SetActive(true);
            weeklyConfig.gameObject.SetActive(true);
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