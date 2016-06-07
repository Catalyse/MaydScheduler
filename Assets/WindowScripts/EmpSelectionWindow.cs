using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;
using CoreSys;

namespace CoreSys.Windows
{
    public class EmpSelectionWindow : MonoBehaviour
    {
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
            for (int i = 0; i < currentBarList.Count; i++)
            {
                
            }
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