using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;
using CoreSys;

namespace CoreSys.Windows
{
    public class EmpSelectionWindow : Window
    {
        public ScheduleWindow parent;
        private List<EmployeeBar> currentBarList = new List<EmployeeBar>();
        public PrefabList prefabs;
        public GameObject empBarSpawnGrid;

        public void GenerateEmpBars(List<EmployeeScheduleWrapper> empList)
        {
            if (currentBarList.Count > 0)
                ClearBars();
            if (empList.Count > 0)
            {
                for (int i = 0; i < empList.Count; i++)
                {
                    GameObject returnedObj = WindowInstantiator.SpawnWindow(prefabs.prefabList[0], empBarSpawnGrid);
                    EmployeeBar bar = returnedObj.GetComponent<EmployeeBar>();
                    bar.EmployeeBarSet(empList[i]);
                    currentBarList.Add(bar);
                }
            }
        }

        public void ReturnSelectedBars()
        {
            List<EmployeeScheduleWrapper> returnList = new List<EmployeeScheduleWrapper>();
            for (int i = 0; i < currentBarList.Count; i++)
            {
                if (currentBarList[i].barToggle.isOn)
                {
                    returnList.Add(currentBarList[i].employeeWrap);
                }
            }
            ClearBars();
            parent.StartAvailLoop(returnList);
        }

        public void Cancel()
        {
            ClearBars();
            parent.AvailChangeCancel();
        }

        private void ClearBars()
        {
            for (int i = 0; i < currentBarList.Count; i++)
            {
                Destroy(currentBarList[i].gameObject);
            }
            currentBarList.Clear();
        }
    }
}