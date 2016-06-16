using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    /// <summary>
    /// This Scheduling algorithm is the guts of this program.
    /// It takes the information that is filled out in the GUI and processes it into a schedule.
    /// This entire class is designed to be used as a threaded process
    /// --//WARNING//-- If this class is NOT threaded your main thread will hang for 5+ seconds, growing longer with more employees.
    /// Due to this class being threaded, it makes it harder to debug in the environment it is designed in, so there are going to be tons of --
    /// -- CoreSystem.ErrorCatch methods to throw debug notes in the event of things not working correctly since we cannot step through it.
    /// </summary>
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
        //                  Master emp list  //Also this might not be needed
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
                    //The day becomes critical if there are not enough employees for the day
                    bool criticalDay = false;
                    //This ratio will be of the % available compared to what is needed.
                    float criticalRatio = 0.0f;
                    //This will pick days based on if they are critical or not, if all critical days have been selected, or there are none, it will pick in order from sunday to saturday(ignoring ones already picked)
                    DailySchedule day = PickDay(pos);
                    //This will generate 
                    Dictionary<int, List<EmployeeScheduleWrapper>> priorityList = GeneratePriorityList(employeePositionDictionary[pos], d);
                    //         priorityListLevel, list of picks
                    Dictionary<int, List<int>> pickList = new Dictionary<int, List<int>>();

                    if (!dailyAvailabilityStatus[pos][d])//If this is false it is a critical day
                    {
                        criticalDay = true;
                    }

                    for (int i = 0; i < priorityList.Count; i++)//init pick list
                        pickList.Add(i, new List<int>());
                    //First we will assign the right number of employees to each day as best we can.
                    for (int i = 0; i < priorityList.Count; i++)
                    {

                    }
                }
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