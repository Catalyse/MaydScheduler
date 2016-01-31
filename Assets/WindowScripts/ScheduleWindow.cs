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
        public GameObject grid, dailyStaffing, weeklyConfig;
        public PrefabList prefabs;

        public ScheduleWindow() { }

        public void StartSchedule();

        private void GenerateSchedule()
        {
            //Week newWeek = SetupWeek();
            SchedulingAlgorithm.GenerateSchedule(newWeek);//This needs to run setup first after its done being tested
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