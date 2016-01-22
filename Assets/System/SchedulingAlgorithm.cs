using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;

namespace CoreSys
{
    public static class SchedulingAlgorithm
    {
        private static List<Employee> employeeList = new List<Employee>();
        private static Schedule schedule = new Schedule();
        //private static Dictionary<int, List<ScheduledEmployee>> scheduleDictionary = new Dictionary<int, List<ScheduledEmployee>>();
        private static Dictionary<int, List<Employee>> availableEmployees = new Dictionary<int, List<Employee>>();//Use this to list all employees available for each day.

        public static void GenerateSchedule(Week week, List<Employee> empList)
        {
            employeeList = empList;
            schedule.employeeList = empList;
            schedule.scheduledWeek = week;
            
            CheckTempDaysOff();
            GenerateAvailabilityList();

            for (int i = 0; i < 7; i++)//Primary Scheduling loop
            {
                //TODO FINISH
            }
        }

        private static void GenerateAvailabilityList()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < employeeList.Count; j++)
                {
                    if (employeeList[j].GetAvailability(i))
                    {
                        availableEmployees[i].Add(employeeList[j]);
                    }
                }
            }
        }

        private static Dictionary<int, List<EmployeeScheduleWrapper>> GenerateSortList(List<EmployeeScheduleWrapper> masterList)
        {
            Dictionary<int, List<EmployeeScheduleWrapper>> returnDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();

            //All categories are based on default shift
            for (int i = 0; i < 5; i++)//because 5 list categories(though this can be expanded easily)
            {
                for (int j = 0; j < masterList.Count; j++)
                {
                    if (masterList[j].scheduledHours < (CoreSystem.defaultShift * (i + 1)) && masterList[j].scheduledHours > (CoreSystem.defaultShift * i))
                    {
                        returnDictionary[i].Add(masterList[j]);
                    }
                }
            }
            return returnDictionary;
        }

        private static List<EmployeeScheduleWrapper> GenerateRestrictionList(List<EmployeeScheduleWrapper> masterList, DayOfWeek day);

        private static void GenerateDay()
        {
            List<EmployeeScheduleWrapper> restrictionList = new List<EmployeeScheduleWrapper>();
            List<EmployeeScheduleWrapper> tier0List = new List<EmployeeScheduleWrapper>();//No shifts (scheduledhours == 0)
            List<EmployeeScheduleWrapper> tier1List = new List<EmployeeScheduleWrapper>();//Up to one shift
            List<EmployeeScheduleWrapper> tier2List = new List<EmployeeScheduleWrapper>();//Up to two shifts
            List<EmployeeScheduleWrapper> tier3List = new List<EmployeeScheduleWrapper>();//Up to three shifts
            List<EmployeeScheduleWrapper> tier4List = new List<EmployeeScheduleWrapper>();//Up to four shifts

        }

        /// <summary>
        /// This method will call the window manager to make a popup occur and show a list of active employee, then ask if they have any extra availability requirements.
        /// </summary>
        private static void CheckTempDaysOff()
        {
            //Call Window for temp days off
        }

        /*
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
        }*/
    }
}