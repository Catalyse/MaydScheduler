using UnityEngine;
using System.Collections;
using CoreSys.Employees;

namespace CoreSys
{
    public class EmployeeScheduleWrapper
    {
        public Employee employee;
        public int maxHours;
        public int scheduledHours;
        //The availability is copied onto this so that it can be modified without effecting the set availability in the employee type
        public Availability availability;

        public EmployeeScheduleWrapper()
        {
            Debug.Log("EmployeeScheduleWrapper created with no employee assigned! EmployeeScheduleWrapper.cs || Default CTOR");
        }

        public EmployeeScheduleWrapper(Employee emp)
        {
            employee = emp;
            maxHours = emp.hourTarget;
            scheduledHours = 0;
            availability = emp.availability;
        }
    }
}