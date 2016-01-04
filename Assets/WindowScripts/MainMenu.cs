using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Windows
{
    public class MainMenu : Window
    {
        /// <summary>
        /// List:
        /// Index   || Name                   || Category
        /// 0       || DailyHourlyStaffing    || New Week
        /// </summary>
        public PrefabList prefabs;
        public GameObject exitPrompt;

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
            WindowInstantiator.SpawnWindow(prefabs.prefabList[2]);
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