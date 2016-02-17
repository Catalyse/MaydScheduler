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
            CoreSystem.LoadCoreSave();
            if (CoreSystem.coreSaveLoaded == false)
            {
                WindowInstantiator.SpawnWindow(prefabs.prefabList[4]);
            }
        }

        public void NewWeek()
        {
            WindowInstantiator.SpawnWindow(prefabs.prefabList[0]);
            Destroy(this.gameObject);
        }

        public void CurrentWeek()
        {
            WindowInstantiator.SpawnWindow(prefabs.prefabList[1]);
            Destroy(this.gameObject);
        }

        public void EmployeeManagement()
        {
            GameObject newWindow = WindowInstantiator.SpawnWindow(prefabs.prefabList[2]);
            newWindow.GetComponent<EmployeeManagement>().GenerateUI();
            Destroy(this.gameObject);
        }

        public void ScheduleHistory()
        {
            WindowInstantiator.SpawnWindow(prefabs.prefabList[3]);
            Destroy(this.gameObject);
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