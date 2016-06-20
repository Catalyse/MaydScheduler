﻿using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    /// <summary>
    /// This Scheduling algorithm is the guts of this program.
    /// It takes the information that is filled out in the GUI and processes it into a schedule.
    /// This entire class is designed to be used as a threaded process to avoid freezing on the main thread as its proccess time is heavy
    /// Due to this class being threaded, it makes it harder to debug in the environment it is designed in, so there are going to be tons of -
    /// - CoreSystem.ErrorCatch methods to throw debug notes in the event of things not working correctly since we cannot step through it.
    /// --//WARNING//-- If this class is NOT threaded your main thread will hang for 5+ seconds, growing longer with more employees.
    /// --//NOTE//-- This class is meant to be very heavy in processing time and relatively high in memory usage(compared to the rest of the program)
    /// --//NOTE//-- If this class is to be adapted without CoreSystem.cs, CoreSystem.ErrorCatch methods will need to be removed. They are used solely for - 
    /// --//TODO//-- Make variable shift algorithm
    /// - debugging and will not cause any issues if removed
    /// </summary>
    public static class NewSchedAlgo
    {
        //A lot of these vars are meant to be temporary within this method so we can minimize the amount of data that needs to be stored.
        //Begin vars  for storage of main thread info
        private static Week week;
        //To know which days have been processed already
        private static List<int> pickedDays = new List<int>();
        //These arent super important, may get rid of them
        private static int weeklyNeededShifts, weeklyAvailShifts;
        //                        position        day, count
        private static Dictionary<int, Dictionary<int, int>> dailyNeededShifts, dailyAvailShifts;
        //                        position        day, status(true = enough shifts available, false = not enough shifts available)
        private static Dictionary<int, Dictionary<int, bool>> dailyAvailabilityStatus = new Dictionary<int, Dictionary<int, bool>>();
        //                  Master emp list  //Also this might not be needed
        private static List<EmployeeScheduleWrapper> employeeList = new List<EmployeeScheduleWrapper>();
        //                        position, employeeList
        private static Dictionary<int, List<EmployeeScheduleWrapper>> employeePositionDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();
        //                        position        day,      empList
        private static Dictionary<int, Dictionary<int, List<EmployeeScheduleWrapper>>> dailyEmployeeList = new Dictionary<int,Dictionary<int,List<EmployeeScheduleWrapper>>>();

        /// <summary>
        /// This is the threaded method called to generate the schedule.
        /// </summary>
        public static void StartScheduleGen()
        {
            week = CoreSystem.week;
            employeeList = week.empList;
            GeneratePositionLists();
            CalcPositionVars();
            AnalyzeResources();
            GenerateSchedule();
        }

        /// <summary>
        /// This will separate the main employee list based on each position.  This can handle as many positions as there are, no limit.
        /// Run count is (number of positions) + (number of employees);
        /// </summary>
        private static void GeneratePositionLists()
        {
            try
            {
                for (int i = 0; i < CoreSystem.positionList.Count; i++)//Create a list for each position
                {
                    employeePositionDictionary.Add(i, new List<EmployeeScheduleWrapper>());
                }

                for (int i = 0; i < week.empList.Count; i++)//Sort employees into the lists made above.
                {
                    employeePositionDictionary[week.empList[i].position].Add(week.empList[i]);
                }
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("GeneratePositionLists() Exception || ScheduleAlgorithm.cs", ex);
            }
        }

        /// <summary>
        /// This will calculate if there are enough employees for the requested shifts for the week and for each day.
        /// </summary>
        private static void CalcPositionVars()
        {
            weeklyNeededShifts = 0;
            weeklyAvailShifts = 0;
            dailyNeededShifts = new Dictionary<int,Dictionary<int,int>>();
            dailyAvailShifts = new Dictionary<int,Dictionary<int,int>>();

            //RunCount = PositionCount * 7
            for (int j = 0; j < CoreSystem.positionList.Count; j++)//Position loop
            {
                for (int i = 0; i < 7; i++)//Day loop
                {
                    DailySchedule day = week.SelectDay(i);//Selects days in order
                    int shifts = day.openNeededShifts[j];
                    shifts += day.closeNeededShifts[j];
                    dailyNeededShifts[j][i] = shifts;
                    weeklyNeededShifts += shifts;
                }
            }

            //yes its a triple loop. shhh
            //RunCount = PositionCount * 7 * NoOfActiveEmployeesInEachPosition
            for (int j = 0; j < CoreSystem.positionList.Count; j++)//positions
            {
                for (int i = 0; i < 7; i++)//days
                {
                    int shifts = 0;
                    for (int k = 0; k < week.empList.Count; k++)//employee list
                    {
                        if (week.empList[k].GetAvailability(i))
                            shifts += 1;
                    }
                    weeklyAvailShifts += shifts;
                    dailyAvailShifts[j][i] = shifts;
                }
            }
        }

        /// <summary>
        /// This will analyze data generated earlier in setup to decide if enough employees are available for each day.
        /// </summary>
        private static void AnalyzeResources()
        {
            if (weeklyNeededShifts > weeklyAvailShifts)//We have less available employee shifts for the week than we need.
                CoreSystem.ErrorCatch("Not enough Available shifts for the week!");
            for (int k = 0; k < CoreSystem.positionList.Count; k++)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (dailyAvailShifts[k][i] < dailyNeededShifts[k][i])
                    {
                        dailyAvailabilityStatus[k][i] = false;
                        CoreSystem.ErrorCatch("Not enough available shifts for day: " + (i + 1));
                    }
                    else
                        dailyAvailabilityStatus[k][i] = true;
                }
            }
        }

        /// <summary>
        /// Primary scheduling algorithm.  This will use information generated in the setup to assign employees to each day.
        /// This is designed for a two shift schedule
        /// </summary>
        private static void GenerateSchedule()
        {
            try
            {
                for (int pos = 0; pos < CoreSystem.positionList.Count; pos++)
                {
                    for (int d = 0; d < 7; d++)
                    {
                        //The day becomes critical if there are not enough employees for the day
                        bool criticalDay = false;
                        //This ratio will be of the % available compared to what is needed.
                        float criticalRatio = 1.0f;
                        float criticalOpen = 1.0f;
                        float criticalClose = 1.0f;
                        //This will pick days based on if they are critical or not, if all critical days have been selected, or there are none, it will pick in order from sunday to saturday(ignoring ones already picked)
                        DailySchedule day = PickDay(pos);
                        //This converts the dayofweek enum to int for use in indexing
                        int dayInt = CoreSystem.ConvertDoWToInt(day.dayOfWeek);
                        //This will generate 
                        Dictionary<int, List<EmployeeScheduleWrapper>> priorityList = GeneratePriorityList(employeePositionDictionary[pos], d);
                        //         priorityListLevel, list of picks
                        Dictionary<int, List<int>> pickList = new Dictionary<int, List<int>>();
                        //init pick list
                        for (int i = 0; i < priorityList.Count; i++)
                            pickList.Add(i, new List<int>());

                        //First we will assign the right number of employees to each day as best we can.
                        //This loop will run until either the correct number of employees is assigned, or until there are no more employees to be scheduled.
                        for (int i = 0; i < priorityList.Count; i++)
                        {
                            for (int j = 0; j < priorityList[i].Count; j++)
                            {
                                if (dailyEmployeeList[dayInt].Count < dailyNeededShifts[pos][dayInt])
                                {
                                    dailyEmployeeList[pos][dayInt].Add(priorityList[i][j]);
                                }
                                else
                                    break;
                            }
                            if (dailyEmployeeList[dayInt].Count >= dailyNeededShifts[pos][dayInt])
                                break;
                        }

                        //If this is false it is a critical day
                        if (!dailyAvailabilityStatus[pos][d])
                        {
                            criticalDay = true;
                            criticalRatio = dailyAvailShifts[pos][dayInt] / dailyNeededShifts[pos][dayInt];
                        }

                        if (criticalDay)//If the day is critical we will modify the number of employees assigned to each shift by the critical ratio.
                        {
                            criticalOpen = day.openNeededShifts[pos];
                            criticalClose = day.closeNeededShifts[pos];
                            criticalOpen /= criticalRatio;
                            criticalClose /= criticalRatio;
                        }

                        //Next we will assign shifts to the available employees we have picked, based on the critical ratio
                    }
                }
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("GenerateSchedule() Exception", ex);
            }
        }

        /// <summary>
        /// This picks a day based on if it is going to be understaffed first, then if all understaffed days are already picked, or there were none in the first place it picks the remaining days in order.
        /// In theory this method will never throw the error at the end.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private static DailySchedule PickDay(int pos)
        {
            for (int i = 0; i < 7; i++)
            {
                if (!dailyAvailabilityStatus[pos][i] && !pickedDays.Contains(i))
                {
                    CoreSystem.ErrorCatch("PickDay() || Priority Picked: " + (i + 1));
                    return week.SelectDay(i);
                }
            }
            for (int i = 0; i < 7; i++)
            {
                if (!pickedDays.Contains(i))
                {
                    CoreSystem.ErrorCatch("PickDay() || Picked: " + (i + 1));
                    return week.SelectDay(i);
                }
            }
            CoreSystem.ErrorCatch("PickDay() Error! || Returning Null");
            return null;//This is bad though, cause then we didnt pick one at all for some reason, this shouldn't ever happen as that means we tried to pick too many days
        }

        /// <summary>
        /// This generates a priority list for each day based on the number of hours each employee has already been scheduled
        /// with the goal being to schedule those with less shifts first to evenly schedule employees.
        /// </summary>
        /// <param name="masterList"></param>
        /// <returns></returns>
        private static Dictionary<int, List<EmployeeScheduleWrapper>> GeneratePriorityList(List<EmployeeScheduleWrapper> masterList, int day)
        {
            int count = 0;
            Dictionary<int, List<EmployeeScheduleWrapper>> returnDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();

            //All categories are based on default shift
            for (int i = 0; i < 5; i++)//because 5 list categories(though this can be expanded easily)
            {
                returnDictionary.Add(i, new List<EmployeeScheduleWrapper>());
                for (int j = 0; j < masterList.Count; j++)
                {
                    if (masterList[j].scheduledHours < (CoreSystem.defaultShift * (i + 1)) && masterList[j].scheduledHours >= (CoreSystem.defaultShift * i) && masterList[j].GetAvailability(day))//Added checking for day so we dont need to recheck availability later.
                    {
                        returnDictionary[i].Add(masterList[j]);
                        count++;
                    }
                }
                if (count >= masterList.Count)
                    break;
            }
            return returnDictionary;
        }
    }
}