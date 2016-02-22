using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;

namespace CoreSys
{
    public static class SchedulingAlgorithm
    {
        private static List<EmployeeScheduleWrapper> employeeList = new List<EmployeeScheduleWrapper>();//Master list holding all employees for the schedule period.
        private static Dictionary<int, List<EmployeeScheduleWrapper>> employeeDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();//Use this as a list of all employees of a given position
        private static Dictionary<int, DailySchedule> currentSchedule = new Dictionary<int, DailySchedule>();

        public static void GenerateScheduleSetup(Week week, List<Employee> empList)
        {
            GenerateWrapperList(empList);//Puts all employees into a wrapper, then sorts them by position into the empDictionary
            CheckTempDaysOff();
            employeeDictionary.Add(0, new List<EmployeeScheduleWrapper>());
            employeeDictionary.Add(1, new List<EmployeeScheduleWrapper>());
            employeeDictionary.Add(2, new List<EmployeeScheduleWrapper>());
            employeeDictionary.Add(3, new List<EmployeeScheduleWrapper>());
            employeeDictionary.Add(4, new List<EmployeeScheduleWrapper>());
            employeeDictionary.Add(5, new List<EmployeeScheduleWrapper>());
            employeeDictionary.Add(6, new List<EmployeeScheduleWrapper>());
        }

        public static Week GenerateSchedule(Week week)
        {
            GenerateScheduleSetup(week, EmployeeStorage.employeeList);
            for (int pos = 0; pos < CoreSystem.positionList.Count; pos++)
            {
                for (int d = 0; d < 7; d++)
                {
                    DailySchedule day = week.SelectDay(d);//make this pick days based on the priority
                    List<EmployeeScheduleWrapper> empList = GenerateAvailabilityList(d, pos);
                    Dictionary<int, List<EmployeeScheduleWrapper>> priorityList = GenerateSortList(empList);
                    List<EmployeeScheduleWrapper> restrictionList = GenerateRestrictionList(empList, week.SelectDay(d));
                    float avgSkill = CalcAvgSkill(empList);
                    
                    Dictionary<int, List<int>> pickList = new Dictionary<int, List<int>>();
                    
                    //Open loop
                    for (int i = 0; i < priorityList.Count; i++)
                    {
                        if(!pickList.ContainsKey(i))
                            pickList.Add(i, new List<int>());
                        if(pickList[i].Count < priorityList[i].Count)//check to make sure we still have valid picks remaining
                        {
                            for (int j = 0; j < (priorityList[i].Count - pickList[i].Count); j++)
                            {
                                pickList[i] = GenerateShiftOpen(pickList[i], priorityList[i], day);//Need to add a debug call to CoreSytem to check if memory in the master dictionary has been changed accordingly
                                if (day.openScheduledShifts.ContainsKey(pos))
                                    day.openScheduledShifts[pos]++;
                                else
                                    day.openScheduledShifts.Add(pos, 1);//Create dictionary entry and add first value
                                if(day.openNeededShifts[pos] <= day.openScheduledShifts[pos])//Though hopefully its never greater than, if it is ive failed.
                                    break;
                            }
                        }
                        if (!day.openScheduledShifts.ContainsKey(pos))//If the open scheduled dictionary doesnt get initialized then no one from that position is needed
                            day.openScheduledShifts.Add(pos, 0);
                        if(day.openNeededShifts[pos] <= day.openScheduledShifts[pos])//Though hopefully its never greater than, if it is ive failed.
                            break;
                    }
                    
                    //Close loop
                    for (int i = 0; i < priorityList.Count; i++)
                    {
                        if (!pickList.ContainsKey(i))
                            pickList.Add(i, new List<int>());
                        if(pickList[i].Count < priorityList[i].Count)//check to make sure we still have valid picks remaining
                        {
                            for (int j = 0; j < (priorityList[i].Count - pickList[i].Count); j++)
                            {
                                pickList[i] = GenerateShiftClose(pickList[i], priorityList[i], day);//Need to add a debug call to CoreSytem to check if memory in the master dictionary has been changed accordingly
                                if (day.closeScheduledShifts.ContainsKey(pos))
                                    day.closeScheduledShifts[pos]++;
                                else
                                    day.closeScheduledShifts.Add(pos, 1);
                                if(day.closeNeededShifts[pos] <= day.closeScheduledShifts[pos])//Though hopefully its never greater than, if it is ive failed.
                                    break;
                            }
                        }
                        if (!day.closeScheduledShifts.ContainsKey(pos))//If the open scheduled dictionary doesnt get initialized then no one from that position is needed
                            day.closeScheduledShifts.Add(pos, 0);
                        if(day.closeNeededShifts[pos] <= day.closeScheduledShifts[pos])//Though hopefully its never greater than, if it is ive failed.
                            break;
                    }
                }
            }
            week.empList = employeeList;
            return week;
        }

        private static List<int> GenerateShiftOpen(List<int> pickList, List<EmployeeScheduleWrapper> sortList, DailySchedule day)
        {
            int pick = GenerateRandomNumber(pickList, sortList.Count);
            int shiftLength = CoreSystem.defaultShift;
            pickList.Add(pick);
            if (EmployeeStorage.GetEmployee(sortList[pick].employee).shiftPreference != shiftLength)
                shiftLength = EmployeeStorage.GetEmployee(sortList[pick].employee).shiftPreference;//Need to add settings on how to handle shift length preferences, but for now its fine
            Shift newShift = new Shift(sortList[pick].employee, day.openTime, (day.openTime + shiftLength), day.date.DayOfWeek);//Need to make this adaptive to shift
            sortList[pick].shiftList.Add(newShift);
            sortList[pick].scheduledHours += shiftLength;
            if(day.shiftDictionary.ContainsKey(sortList[pick]))
            {
                day.shiftDictionary[sortList[pick]].Add(newShift);
            }
            else 
            {
                List<Shift> newShiftList = new List<Shift>();
                newShiftList.Add(newShift);
                day.shiftDictionary.Add(sortList[pick], newShiftList);
            }
            return pickList;
        }

        private static List<int> GenerateShiftClose(List<int> pickList, List<EmployeeScheduleWrapper> sortList, DailySchedule day)
        {
            int pick = GenerateRandomNumber(pickList, sortList.Count);
            int shiftLength = CoreSystem.defaultShift;
            pickList.Add(pick);
            if (EmployeeStorage.GetEmployee(sortList[pick].employee).shiftPreference != shiftLength)
                shiftLength = EmployeeStorage.GetEmployee(sortList[pick].employee).shiftPreference;//Need to add settings on how to handle shift length preferences, but for now its fine
            Shift newShift = new Shift(sortList[pick].employee, day.closeTime - shiftLength, day.closeTime, day.date.DayOfWeek);//Need to make this adaptive to shift
            sortList[pick].shiftList.Add(newShift);
            sortList[pick].scheduledHours += shiftLength;
            if (day.shiftDictionary.ContainsKey(sortList[pick]))
            {
                day.shiftDictionary[sortList[pick]].Add(newShift);
            }
            else
            {
                List<Shift> newShiftList = new List<Shift>();
                newShiftList.Add(newShift);
                day.shiftDictionary.Add(sortList[pick], newShiftList);
            }
            return pickList;
        }

        /// <summary>
        /// Runs a loop to find a random number that hasnt already been chosen
        /// </summary>
        private static int GenerateRandomNumber(List<int> pickList, int max)
        {
            int temp = 0;
            if(pickList.Count < max)//This is a partial fix.  This will check to see if the max is less than the number of picks already made theoretically skipping 
            {
                while(true)//yes i know infinite stfu //TODO FIX THIS
                {
                    temp = CoreSystem.RandomInt(max);
                    if (!pickList.Contains(temp))
                    {
                        return temp;
                    }                   
                }
            }
            return -1;//If the check fails return -1
        }

        /// <summary>
        /// Calculates the average skill level of a list of employees
        /// </summary>
        /// <param name="empList"></param>
        /// <returns></returns>
        private static float CalcAvgSkill(List<EmployeeScheduleWrapper> empList)
        {
            float average = 0.0f;
            for (int i = 0; i < empList.Count; i++)
            {
                average += EmployeeStorage.GetEmployee(empList[i].employee).skillLevel;
            }
            average = average / empList.Count;
            return average;
        }

        /// <summary>
        /// This method accepts a list of employees, then adds all active employees to a wrapper for more information to be placed on top of them without affecting the base saved data
        /// </summary>
        /// <param name="empList">Unsorted list of all employees in the system</param>
        private static void GenerateWrapperList(List<Employee> empList)
        {
            for (int i = 0; i < empList.Count; i++)
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
        /// This method makes wrappers for both employee position types
        /// </summary>
        private static List<EmployeeScheduleWrapper> GenerateAvailabilityList(int day, int pos)
        {
            List<EmployeeScheduleWrapper> returnList = new List<EmployeeScheduleWrapper>();
            for (int j = 0; j < employeeList.Count; j++)
            {
                //Check to make sure they can 1. work that day and 2. have hours left to be scheduled
                if (employeeList[j].GetAvailability(day) && employeeList[j].scheduledHours < employeeList[j].maxHours && employeeList[j].position == pos)
                {
                    returnList.Add(employeeList[j]);
                    employeeDictionary[day].Add(employeeList[j]);//This is extra data, check if useful
                }
            }
            return returnList;
        }
        
        private static void CalculateManHours(List<DailySchedule> dayList)
        {
            for(int j = 0; j < CoreSystem.positionList.Count; j++)
            {
                for(int i = 0; i < dayList.Count; i++)
                {
                    //int dailyEmpsNeeded = dayList
                }
            }
        }

        private static Dictionary<int, List<EmployeeScheduleWrapper>> GenerateSortList(List<EmployeeScheduleWrapper> masterList)
        {
            Dictionary<int, List<EmployeeScheduleWrapper>> returnDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();

            //All categories are based on default shift
            for (int i = 0; i < 5; i++)//because 5 list categories(though this can be expanded easily)
            {
                returnDictionary.Add(i, new List<EmployeeScheduleWrapper>());
                for (int j = 0; j < masterList.Count; j++)
                {
                    if (masterList[j].scheduledHours < (CoreSystem.defaultShift * (i + 1)) && masterList[j].scheduledHours >= (CoreSystem.defaultShift * i))
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

        private static RestrictedReturn CheckRestricted(EmployeeScheduleWrapper emp, DailySchedule day, bool open)
        {
            RestrictedReturn empStatus = new RestrictedReturn();
            if (open)//open shift check
            {
                if (emp.availability[day.dayOfWeek].startTime > day.openTime)//cannot start, reject.
                {
                    empStatus.canWork = false;
                    return empStatus;
                }
                else//Figure out how long they can work//minshift 4 hours
                {
                    if (day.openTime + CoreSystem.defaultShift <= emp.availability[day.dayOfWeek].endTime)
                    {//No restriction
                        empStatus.canWork = true;
                        empStatus.maxShift = 8;
                        return empStatus;
                    }
                    else
                    {//loop till we find when they can start working as long as it is greater than the min shift length allowed
                        int i = (day.openTime + CoreSystem.defaultShift);
                        for (; i > (day.openTime + CoreSystem.minShift); i--)
                        {
                            if (i <= emp.availability[day.dayOfWeek].endTime)
                            {
                                empStatus.canWork = true;
                                empStatus.maxShift = (i - day.openTime);
                                return empStatus;
                            }
                        }//If it exits the loop they cannot work shift is too short
                        empStatus.canWork = false;
                        return empStatus;
                    }
                }
            }
            else//close shift check
            {
                if (emp.availability[day.dayOfWeek].endTime < day.closeTime)//cannot close, reject.
                {
                    empStatus.canWork = false;
                    return empStatus;
                }
                else//Figure out how long they can work//minshift 4 hours
                {
                    if (day.closeTime - CoreSystem.defaultShift >= emp.availability[day.dayOfWeek].startTime)
                    {//No restriction
                        empStatus.canWork = true;
                        empStatus.maxShift = 8;
                        return empStatus;
                    }
                    else
                    {//loop till we find when they can start working as long as it is greater than the min shift length allowed
                        int i = (day.openTime + CoreSystem.defaultShift);
                        for (; i > (day.openTime + CoreSystem.minShift); i--)
                        {
                            if (i <= emp.availability[day.dayOfWeek].endTime)
                            {
                                empStatus.canWork = true;
                                empStatus.maxShift = (i - day.openTime);
                                return empStatus;
                            }
                        }//If it exits the loop they cannot work shift is too short
                        empStatus.canWork = false;
                        return empStatus;
                    }
                }
            }
        }

        /// <summary>
        /// This method will call the window manager to make a popup occur and show a list of active employee, then ask if they have any extra availability requirements.
        /// </summary>
        private static void CheckTempDaysOff()
        {
            //Call Window for temp days off
        }

        /* //This is being replaced with the total hours for the week
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
