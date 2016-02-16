using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Errors;

namespace CoreSys.Employees
{
    public static class EmployeeStorage
    {
        public static List<Employee> employeeList = new List<Employee>();

        public static void Start()
        {
            List<Employee> temp = EmpListSerializer.DeserializeEmpList();
            if (temp != null)
            {
                employeeList = temp;
            }
            else
                Debug.Log("No employee list found");
        }

        public static void AddEmployee(Employee toAdd)
        {
            employeeList.Add(toAdd);
            SortList();
        }

        public static void OverWriteList(List<Employee> empList)
        {
            employeeList = empList;
            SortList();
        }

        public static Employee GetEmployee(string name)
        {
            try
            {
                for (int i = 0; i < employeeList.Count; i++)
                {
                    if (employeeList[i].empLastName == name)
                        return employeeList[i];
                }
                throw new EmpNotFoundErr();
            }
            catch (EmpNotFoundErr e)
            {
                e.ThrowMsg();
                return null;
            }
        }

        private static void SortList()
        {
            //Starts false so if list is already in order do nothing
            bool sortAgain = false;
            Employee current;
            Employee next;
            do
            {
                if (employeeList.Count > 1)
                {
                    sortAgain = false;
                    for (int i = 0; i < employeeList.Count - 1; i++)
                    {
                        current = employeeList[i];
                        next = employeeList[i + 1];
                        if (current.empLastName.CompareTo(next.empLastName) > 0)
                        {
                            employeeList[i] = next;
                            employeeList[i + 1] = current;
                            sortAgain = true;
                        }
                    }
                }
            }
            while (sortAgain == true);
        }

        public static void ClearList()
        {
            employeeList.Clear();
            EmpListSerializer.DeleteEmpListFile();
        }

        public static void OnDestroy()
        {
            if (employeeList.Count > 0)
            {
                EmpListSerializer.SerializeEmpList(employeeList);
            }
            else
                Debug.Log("No employee list to serialize!");
        }
    }
}