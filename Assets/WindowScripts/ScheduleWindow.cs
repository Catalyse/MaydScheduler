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
        private bool waiting = false;

        public ScheduleWindow() { }

        public void Start()
        {
            StartSchedule();
        }

        public void Update()
        {
            if (waiting == true)//Once the schedule setup is done we want to start running update to check for when generation is complete
            {
                if (!CoreSystem.currentlyProcessing)//Once this becomes false 
                {
                    currentWeek = CoreSystem.week;
                    DrawSchedule();
                    CoreSystem.weekList[currentWeek.startDate] = currentWeek;
                    CoreSystem.CoreSettingsChanged();
                    waiting = false;
                }
            }
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
            CoreSystem.GenerateSchedule(currentWeek);
			waiting = true;
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