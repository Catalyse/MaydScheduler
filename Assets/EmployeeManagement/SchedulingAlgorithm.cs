using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;

namespace CoreSys
{
    public static class SchedulingAlgorithm
    {
        private static List<Employee> employeeList = new List<Employee>();
        private static Schedule schedule;

        public static void GenerateSchedule(Week week, List<Employee> empList)
        {
            schedule.employeeList = empList;
            schedule.scheduledWeek = week;
            List<int> scheduleOrder = 
        }
    }
}