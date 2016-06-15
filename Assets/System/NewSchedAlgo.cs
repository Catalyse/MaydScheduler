using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public static class NewSchedAlgo
    {
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
        //                  Master emp list
        private static List<EmployeeScheduleWrapper> employeeList = new List<EmployeeScheduleWrapper>();
        //                        position, employeeList
        private static Dictionary<int, List<EmployeeScheduleWrapper>> employeePositionDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();

        public static void StartScheduleGen()
        {
            week = CoreSystem.week;
            employeeList = week.empList;
            GeneratePositionLists();
            CalcPositionVars();
            GenerateSchedule();
        }

        private static void GeneratePositionLists()
        {
            try
            {
                for (int i = 0; i < CoreSystem.positionList.Count; i++)
                {
                    employeePositionDictionary.Add(i, new List<EmployeeScheduleWrapper>());
                }

                for (int i = 0; i < week.empList.Count; i++)
                {
                    employeePositionDictionary[week.empList[i].position].Add(week.empList[i]);
                }
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("GeneratePositionLists() Exception || ScheduleAlgorithm.cs", ex);
            }
        }

        private static void CalcPositionVars()
        {
            weeklyNeededShifts = 0;
            weeklyAvailShifts = 0;
            dailyNeededShifts = new Dictionary<int,Dictionary<int,int>>();
            dailyAvailShifts = new Dictionary<int,Dictionary<int,int>>();

            for (int j = 0; j < CoreSystem.positionList.Count; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    DailySchedule day = week.SelectDay(i);
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
                    for (int k = 0; k < week.empList.Count; k++)
                    {
                        if (week.empList[k].GetAvailability(i))
                            shifts += 1;
                    }
                    weeklyAvailShifts += shifts;
                    dailyAvailShifts[j][i] = shifts;
                }
            }
        }

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

        private static void GenerateSchedule()
        {
            for (int pos = 0; pos < CoreSystem.positionList.Count; pos++)
            {
                for (int d = 0; d < 7; d++)
                {
                    DailySchedule day = PickDay(pos);
                    Dictionary<int, List<EmployeeScheduleWrapper>> priorityList = GeneratePriorityList(employeePositionDictionary[pos], d);
                    //         priorityListLevel, list of picks
                    Dictionary<int, List<int>> pickList = new Dictionary<int, List<int>>();

                    for (int i = 0; i < priorityList.Count; i++)//init pick list
                        pickList.Add(i, new List<int>());
                    //First we will assign the right number of employees to each day as best we can.
                    for (int i = 0; i < priorityList.Count; i++)
                    {

                    }
                }
            }
        }

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