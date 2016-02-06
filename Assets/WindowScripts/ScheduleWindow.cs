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
        public GameObject grid, dailyStaffing, weeklyConfig;
        public PrefabList prefabs;

        public ScheduleWindow() { }

        public void StartSchedule()
        {
            currentWeek = new Week();
            weeklyConfig.SetActive(true);
            weeklyConfig.GetComponent<WeeklyConfig>().SetWeek(currentWeek);
        }

        public void DailyStaffing(Week week)
        {
            dailyStaffing.SetActive(true);
        }

        public void GenerateSchedule(Week week)
        {
            //Week newWeek = SetupWeek();
            SchedulingAlgorithm.GenerateSchedule(week);//This needs to run setup first after its done being tested
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