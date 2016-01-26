using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;

namespace CoreSys
{
    public static class SchedulingAlgorithm
    {
        private static List<EmployeeScheduleWrapper> employeeList = new List<EmployeeScheduleWrapper>();
        private static Schedule schedule = new Schedule();
        private static Dictionary<int, List<EmployeeScheduleWrapper>> employeeDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();//Use this to list all employees available for each day.

        public static void GenerateScheduleSetup(Week week, List<Employee> empList)
        {
            GenerateWrapperList(empList);
            schedule.employeeList = empList;
            schedule.scheduledWeek = week;
            
            CheckTempDaysOff();
            GenerateAvailabilityList();
        }
        
        public static void GenerateSchedule()
        {
            for(int d = 0; d < 7; d++)
            {
                List<EmployeeScheduleWrapper> empList = GenerateAvailabilityList(d);
            }
        }

        /// <summary>
        /// This method accepts a list of employees, then adds all active employees to a wrapper for more information to be placed on top of them without affecting the base saved data
        /// </summary>
        /// <param name="empList">Unsorted list of all employees in the system</param>
        private static void GenerateWrapperList(List<Employee> empList)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (empList[i].active)//Check if the employee is active
                {
                    EmployeeScheduleWrapper newWrapper = new EmployeeScheduleWrapper(empList[i]);
                    employeeList.Add(newWrapper);
                }
            }
        }

        /// <summary>
        /// This method iterates through each employee on the active list, and finds out if they are available for each day of the week.
        /// </summary>
        private static List<EmployeeScheduleWrapper GenerateAvailabilityList(int day)
        {
            List<EmployeeScheduleWrapper> returnList = new List<EmployeeScheduleWrapper>();
            for (int j = 0; j < employeeList.Count; j++)
            {
                //Check to make sure they can 1. work that day and 2. have hours left to be scheduled
                if (employeeList[j].GetAvailability(day) && employeeList[j].scheduledHours < employeeList[j].maxHours)
                {
                    returnList.Add(employeeList[j]);
                    employeeDictionary[day].Add(employeeList[j]);//This is extra data, check if useful
                }
            }
            return 
        }
        
        private static void CalculateManHours(List<DailySchedule> dayList)
        {
            for(int i = 0; i < dayList.Count; i++)
            {
                int dailyEmpsNeeded = dayList
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

        private static List<EmployeeScheduleWrapper> GenerateRestrictionList(List<EmployeeScheduleWrapper> masterList, DailySchedule day)
        {
            List<EmployeeScheduleWrapper> restrictionList = new List<EmployeeScheduleWrapper>();
            switch (day.dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    for (int i = 0; i < masterList.Count; i++)
                    {
                        if (masterList[i].availability.sunday.openAvail == false)
                        {
                            restrictionList.Add(masterList[i]);
                        }
                    }
                    return restrictionList;
                case DayOfWeek.Monday:
                    for (int i = 0; i < masterList.Count; i++)
                    {
                        if (masterList[i].availability.monday.openAvail == false)
                        {
                            restrictionList.Add(masterList[i]);
                        }
                    }
                    return restrictionList;
                case DayOfWeek.Tuesday:
                    for (int i = 0; i < masterList.Count; i++)
                    {
                        if (masterList[i].availability.tuesday.openAvail == false)
                        {
                            restrictionList.Add(masterList[i]);
                        }
                    }
                    return restrictionList;
                case DayOfWeek.Wednesday:
                    for (int i = 0; i < masterList.Count; i++)
                    {
                        if (masterList[i].availability.wednesday.openAvail == false)
                        {
                            restrictionList.Add(masterList[i]);
                        }
                    }
                    return restrictionList;
                case DayOfWeek.Thursday:
                    for (int i = 0; i < masterList.Count; i++)
                    {
                        if (masterList[i].availability.thursday.openAvail == false)
                        {
                            restrictionList.Add(masterList[i]);
                        }
                    }
                    return restrictionList;
                case DayOfWeek.Friday:
                    for (int i = 0; i < masterList.Count; i++)
                    {
                        if (masterList[i].availability.friday.openAvail == false)
                        {
                            restrictionList.Add(masterList[i]);
                        }
                    }
                    return restrictionList;
                case DayOfWeek.Saturday:
                    for (int i = 0; i < masterList.Count; i++)
                    {
                        if (masterList[i].availability.saturday.openAvail == false)
                        {
                            restrictionList.Add(masterList[i]);
                        }
                    }
                    return restrictionList;
                default:
                    Debug.Log("This error is literally impossible to get to, BUT THAT BEING SAID || ERROR || SchedulingAlgorithm.cs || GenerateRestrictionList || Default case chosen");
                    return null;//Totally fucked up return but again, should be impossible to hit this error.
            }
        }

        private static void GenerateDay(DailySchedule day, List<EmployeeScheduleWrapper> empList)
        {
            List<EmployeeScheduleWrapper> restrictionList = new List<EmployeeScheduleWrapper>();//Holds a list of people who do not have open availability this day, but are able to work.
            //Dictionary of the scheduling tiers.  Tier 0: no shifts || Tier 1: Up to one shift of default length || Tier 2: up to 2 || Tier 3: up to 3 || Tier 4: up to 4
            Dictionary<int, List<EmployeeScheduleWrapper>> tierList = new Dictionary<int, List<EmployeeScheduleWrapper>>();

            restrictionList = GenerateRestrictionList(empList, day);
            tierList = GenerateSortList(empList);
        }

        /// <summary>
        /// This method will call the window manager to make a popup occur and show a list of active employee, then ask if they have any extra availability requirements.
        /// </summary>
        private static void CheckTempDaysOff()
        {
            //Call Window for temp days off
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
