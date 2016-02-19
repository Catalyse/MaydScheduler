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

        public void Start()
        {
            StartSchedule();
        }

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
            dailyStaffing.GetComponent<DailyStaffing>().SetWeeklyConfig();
        }

        public void GenerateSchedule()
        {
            dailyStaffing.SetActive(false);
            currentWeek = SchedulingAlgorithm.GenerateSchedule(currentWeek);
            DrawSchedule();
            CoreSystem.weekList[currentWeek.startDate] = currentWeek;
            CoreSystem.CoreSettingsChanged();
        }

        public void DrawSchedule()
        {
            title.text = "Schedule For the Week of " + currentWeek.startDate.ToShortDateString();
            for (int i = 0; i < currentWeek.empList.Count; i++)
            {
                GameObject newWindow = WindowInstantiator.SpawnWindow(prefabs.prefabList[0], grid);
                EmployeeScheduleBar bar = newWindow.GetComponent<EmployeeScheduleBar>();
                bar.SetBar(currentWeek.empList[i]);
            }
        }
    }
}