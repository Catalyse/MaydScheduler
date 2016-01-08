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
        private static Dictionary<int, List<ScheduledEmployee>> scheduleDictionary = new Dictionary<int, List<ScheduledEmployee>>();
        private static Dictionary<int, List<Employee>> availableEmployees = new Dictionary<int, List<Employee>>();//Use this to list all employees available for each day.

        public static void GenerateSchedule(Week week, List<Employee> empList)
        {
            employeeList = empList;
            schedule.employeeList = empList;
            schedule.scheduledWeek = week;
            List<int> sScheduleOrder = new List<int>(calcScheduleOrder(week, 0));//ss
            List<int> eScheduleOrder = new List<int>(calcScheduleOrder(week, 1));//es
            CheckTempDaysOff();

            for (int i = 0; i < 7; i++)//Primary Scheduling loop
            {

            }
        }

        private void GenerateDay()
        {

        }

        /// <summary>
        /// This method will call the window manager to make a popup occur and show a list of active employee, then ask if they have any extra availability requirements.
        /// </summary>
        private static void CheckTempDaysOff()
        {

        }

        private static List<int> calcScheduleOrder(Week week, int type)
        {
            List<int> retList = new List<int>();
            if (type == 0)
            {//SS position type
                for (int i = 0; i < 7; i++)
                {
                    retList.Add(week.sDailyTotalNeeds[i]);
                }
                bool sortAgain = false;
                int current;
                int next;
                do
                {
                    for (int i = 0; i < 6; i++)
                    {
                        current = retList[i];
                        next = retList[i + 1];
                        if (current < next)
                        {
                            retList[i] = next;
                            retList[i + 1] = current;
                            sortAgain = true;
                        }
                    }
                } while (sortAgain);
            }
            else
            {//For experience position type // TODO make this generic
                for (int i = 0; i < 7; i++)
                {
                    retList.Add(week.eDailyTotalNeeds[i]);
                }
            }
            return retList;
        }
    }
}