using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CoreSys;
using CoreSys.Employees;

namespace CoreSys.Windows
{
    public class ScheduleWindow : Window
    {
        public Text title;
        public Week currentWeek;
        public GameObject grid, dailyStaffing, weeklyConfig, chooseWeek, empSelection, availWindow, availPrompt;
        public PrefabList prefabs;
        private int currentEmp = 0;
        private bool waiting = false;
        private List<EmployeeScheduleWrapper> AvailChangeList;

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

        /// <summary>
        /// This method accepts a list of employees, then adds all active employees to a wrapper for more information to be placed on top of them without affecting the base saved data
        /// </summary>
        /// <param name="empList">Unsorted list of all employees in the system</param>
        public void GenerateWrapperList()
        {
            List<Employee> empList = EmployeeStorage.employeeList;
            for (int i = 0; i < empList.Count; i++)
            {
                if (empList[i].active)//Check if the employee is active
                {
                    EmployeeScheduleWrapper newWrapper = new EmployeeScheduleWrapper(empList[i]);
                    currentWeek.empList.Add(newWrapper);
                }
            }
        }

        /// TEMP AVAILABILITY CHANGE SECTION /// =============================================================================================================================== /

        /// <summary>
        /// This will prompt the user to see if anyone needs a temporary change to availability
        /// </summary>
        public void AvailabilityPrompt()
        {
            availPrompt.SetActive(true);
        }

        /// <summary>
        /// This method will call the window manager to make a popup occur and show a list of active employee, then ask if they have any extra availability requirements.
        /// This will start the iteration through all employees needing a change.
        /// </summary>
        public void CheckTempDaysOff()
        {
            availPrompt.SetActive(false);
            empSelection.SetActive(true);
            empSelection.GetComponent<EmpSelectionWindow>().GenerateEmpBars(currentWeek.empList);
        }

        /// <summary>
        /// This will loop through the employees selected to have an availability change
        /// </summary>
        /// <param name="empList"></param>
        public void StartAvailLoop(List<EmployeeScheduleWrapper> empList)
        {
            empSelection.SetActive(false);
            AvailChangeList = empList;
            currentEmp = 0;
            availWindow.SetActive(true);
            availWindow.GetComponent<AvailabilityWindow>().SetToggles(AvailChangeList[currentEmp].GetEntireAvail(), AvailChangeList[currentEmp].lName + ", " + AvailChangeList[currentEmp].fName);
        }

        /// <summary>
        /// This is a callback from the loop to change temp avail
        /// </summary>
        /// <param name="avail"></param>
        public void AvailLoopCallback(Availability avail)
        {
            AvailChangeList[currentEmp].SetTempAvailability(avail);
            currentEmp++;
            if (currentEmp >= AvailChangeList.Count)//it really shouldnt ever be higher but you know, code n shit.
                CompleteAvailChange();
            else
                availWindow.GetComponent<AvailabilityWindow>().SetToggles(AvailChangeList[currentEmp].GetEntireAvail(), AvailChangeList[currentEmp].lName + ", " + AvailChangeList[currentEmp].fName);
        }

        /// <summary>
        /// THis is called on completion of the availability loop
        /// </summary>
        private void CompleteAvailChange()
        {
            availWindow.SetActive(false);
            for (int i = 0; i < AvailChangeList.Count; i++)
            {
                currentWeek.empList[currentWeek.empList.IndexOf(AvailChangeList[i])] = AvailChangeList[i];//replace the employee with the one with temp avail.  Prolly a more efficient way to do this but thats for later.
            }
            GenerateSchedule();
        }

        /// <summary>
        /// THis cancels the loop where its at, we may need to do more cleanup versus just disabling the windows
        /// </summary>
        public void AvailChangeCancel()
        {
            availWindow.SetActive(false);
            empSelection.SetActive(false);
            availPrompt.SetActive(false);
            GenerateSchedule();
        }

        /// END TEMP AVAILABILITY CHANGE SECTION /// =========================================================================================================================== /

        public void GenerateFinalSetup()
        {
            dailyStaffing.SetActive(false);
            GenerateWrapperList();
            AvailabilityPrompt();
        }

        private void GenerateSchedule()
        {
            waiting = true;
            CoreSystem.GenerateSchedule(currentWeek);
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