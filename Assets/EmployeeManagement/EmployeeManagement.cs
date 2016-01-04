using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;
using CoreSys.Windows;

namespace CoreSys.Employees
{
    public class EmployeeManagement : Window
    {
        /// <summary>
        /// List:
        /// Index   || Name                 || Category
        /// 0       || EmployeeBar          || New Week
        /// </summary>
        public PrefabList prefabs;
        public GameObject empBarSpawnGrid, addEmpObject;
        public ToggleGroup empBarToggleGroup;

        private List<EmployeeBar> currentBarList = new List<EmployeeBar>();

        public void GenerateUI()
        {
            GenerateEmpBars(EmployeeStorage.employeeList);
        }

        public void AddEmployeeWindow()
        {
            addEmpObject.SetActive(true);
        }

        public void AddEmployee(Employee emp)
        {
            EmployeeStorage.AddEmployee(emp);
            GenerateEmpBars(EmployeeStorage.employeeList);
            addEmpObject.SetActive(false);
        }

        public void EditEmployee(int emp)
        {

        }

        public void RemoveEmployee(int emp)
        {
            
        }

        private void ClearBars()
        {
            for (int i = 0; i < currentBarList.Count; i++)
            {
                Destroy(currentBarList[i].gameObject);
            }
            currentBarList.Clear();
        }

        public void GenerateEmpBars(List<Employee> empList)
        {
            if (currentBarList.Count > 0)
                ClearBars();
            if(empList.Count > 0)
            {
                for (int i = 0; i < empList.Count; i++)
                {
                    GameObject returnedObj = WindowInstantiator.SpawnWindow(prefabs.prefabList[0], empBarSpawnGrid);
                    EmployeeBar bar = returnedObj.GetComponent<EmployeeBar>();
                    bar.barToggle.group = empBarToggleGroup;
                    bar.EmployeeBarSet(empList[i]);
                    currentBarList.Add(bar);
                }
            }
        }
    }
}