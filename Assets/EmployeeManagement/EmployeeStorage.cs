using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Errors;

namespace CoreSys.Employees
{
    public class EmployeeStorage : MonoBehaviour
    {
        public int employeeCount;
        public List<Employee> employeeList;

        public EmployeeStorage() { }

        public void Start()
        {
            EmployeeStorage temp = EmpListSerializer.DeserializeEmpList();
            if (temp != null)
            {
                employeeCount = temp.employeeCount;
                employeeList = temp.employeeList;
            }
            else
                Debug.Log("No employee list found");
        }

        public void AddEmployee(Employee toAdd)
        {
            employeeList.Add(toAdd);
            employeeCount++;
            SortList();
        }

        public void OverWriteList(List<Employee> empList)
        {
            employeeList = empList;
            employeeCount = empList.Count;
            SortList();
        }

        public Employee GetEmployee(string name)
        {
            try
            {
                for (int i = 0; i < employeeList.Count; i++)
                {
                    if (employeeList[i].empName == name)
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

        private void SortList()
        {
            //Starts false so if list is already in order do nothing
            bool sortAgain = false;
            Employee current;
            Employee next;
            do
            {
                for (int i = 0; i < employeeList.Count - 1; i++)
                {
                    current = employeeList[i];
                    next = employeeList[i + 1];
                    if (current.name.CompareTo(next.name) > 0)
                    {
                        employeeList[i] = next;
                        employeeList[i + 1] = current;
                        sortAgain = true;
                    }
                }
            }
            while (sortAgain == true);
        }

        public void OnDestroy()
        {
            if (employeeList.Count > 0)
            {
                EmpListSerializer.SerializeEmpList(this);
            }
            else
                Debug.Log("No employee list to serialize!");
        }
    }
}