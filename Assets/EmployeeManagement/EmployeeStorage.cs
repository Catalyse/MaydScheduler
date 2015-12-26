using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Errors;

namespace CoreSys
{
    public class EmployeeStorage : MonoBehaviour
    {
        public int employeeCount;
        public List<Employee> employeeList;

        public EmployeeStorage() 
        {

        }

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
    }
}