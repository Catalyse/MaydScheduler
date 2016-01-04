using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;

namespace CoreSys.Windows
{
    public class EmployeeManagement : MonoBehaviour
    {
        /// <summary>
        /// List:
        /// Index   || Name                 || Category
        /// 0       || EmployeeBar          || New Week
        /// </summary>
        public PrefabList prefabs;
        public GameObject empBarSpawnGrid;
        public ToggleGroup empBarToggleGroup;
        private EmployeeStorage currentEmployeeList;
        private List<EmployeeBar> currentBarList = new List<EmployeeBar>();

        public void GenerateUI()
        {
            currentEmployeeList = transform.root.GetComponent<EmployeeStorage>();
            GenerateEmpBars(currentEmployeeList.employeeList);
        }

        public void AddEmployee()
        {

        }

        public void EditEmployee(int emp)
        {

        }

        public void RemoveEmployee(int emp)
        {

        }

        public void GenerateEmpBars(List<Employee> empList)
        {
            for (int i = 0; i < empList.Count; i++)
            {
                GameObject returnedObj = WindowInstantiator.SpawnWindow(prefabs.prefabList[0],empBarSpawnGrid);
                EmployeeBar bar = returnedObj.GetComponent<EmployeeBar>();
                bar.barToggle.group = empBarToggleGroup;
                currentBarList.Add(bar);
            }
        }
    }
}