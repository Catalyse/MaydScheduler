using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CoreSys.Employees;

namespace CoreSys.Windows
{
    public class MainMenu : Window
    {
        /// <summary>
        /// List:
        /// Index   || Name                   || Category
        /// 0       || DailyHourlyStaffing    || Schedule Creation
        /// 1
        /// 2       || EmpMGMTWindow          || Employee Management
        /// </summary>
        public PrefabList prefabs;
        public GameObject exitPrompt;

        public void Start()
        {
            CoreSystem.LoadCoreSettings();
            if (CoreSystem.coreSettingsLoaded == false)
            {
                WindowInstantiator.SpawnWindow(prefabs.prefabList[4]);
            }
        }

        public void NewWeek()
        {
            WindowInstantiator.SpawnWindow(prefabs.prefabList[0]);
            CloseWindow();
        }

        public void CurrentWeek()
        {
            WindowInstantiator.SpawnWindow(prefabs.prefabList[1]);
            CloseWindow();
        }

        public void EmployeeManagement()
        {
            GameObject newWindow = WindowInstantiator.SpawnWindow(prefabs.prefabList[2]);
            newWindow.GetComponent<EmployeeManagement>().GenerateUI();
            CloseWindow();
        }

        public void ScheduleHistory()
        {
            WindowInstantiator.SpawnWindow(prefabs.prefabList[3]);
            CloseWindow();
        }

        public void ExitApp()
        {
            exitPrompt.SetActive(true);
        }

        public void CancelExit()
        {
            exitPrompt.SetActive(false);
        }

        public void ConfirmExit()
        {
            Application.Quit();
        }
    }
}