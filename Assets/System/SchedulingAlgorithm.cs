using System;
using System.Collections.Generic;
using CoreSys.Employees;

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
    ///              - debugging in the unity engine since the scheduling algorithm does not contain the unity library(which is not threadsafe)
    /// --//TODO//-- Make variable(multiple) shift algorithm
    /// </summary>
    public static class SchedulingAlgorithm
    {
        //A lot of these vars are meant to be temporary within this method so we can minimize the amount of data that needs to be stored.
        //Begin vars  for storage of main thread info
        private static Week week;
        //To know which days have been processed already
        private static List<int> pickedDays = new List<int>();
        //These arent super important, may get rid of them
        private static Dictionary<int, int> weeklyNeededShifts = new Dictionary<int,int>(), weeklyAvailShifts = new Dictionary<int,int>();
        //                        position, status(true = enough shifts available, false = not enough shifts available)
        private static Dictionary<int, bool> weeklyAvailabilityStatus = new Dictionary<int, bool>();
        //                        position        day, count
        private static Dictionary<int, Dictionary<int, int>> dailyNeededShifts = new Dictionary<int,Dictionary<int,int>>(), dailyAvailShifts = new Dictionary<int,Dictionary<int,int>>();
        //                        position        day, status(true = enough shifts available, false = not enough shifts available)
        private static Dictionary<int, Dictionary<int, bool>> dailyAvailabilityStatus = new Dictionary<int, Dictionary<int, bool>>();
        //                        position, employeeList
        private static Dictionary<int, List<EmployeeScheduleWrapper>> employeePositionDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();

        /// <summary>
        /// This is the threaded method called to generate the schedule.
        /// </summary>
        public static void StartScheduleGen()
        {
            try
            {
                week = CoreSystem.week;
                GeneratePositionLists();
                CalcPositionVars();
                AnalyzeResources();
                GenerateSchedule();
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("StartScheduleGen() Exception || This is likely a fallthrough error, debug", ex);
            }
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
            try
            {
                //RunCount = PositionCount * 7
                for (int pos = 0; pos < CoreSystem.positionList.Count; pos++)//Position loop
                {
                    weeklyNeededShifts.Add(pos, 0);
                    dailyNeededShifts.Add(pos, new Dictionary<int, int>());
                    for (int i = 0; i < 7; i++)//Day loop
                    {
                        DailySchedule day = week.SelectDay(i);//Selects days in order
                        int shifts = 0;
                        day.openNeededShifts.TryGetValue(pos, out shifts);
                        shifts += day.closeNeededShifts[pos];
                        dailyNeededShifts[pos].Add(i, shifts);
                        weeklyNeededShifts[pos] += shifts;
                    }
                }

                //yes its a triple loop. shhh
                //RunCount = PositionCount * 7 * NoOfActiveEmployeesInEachPosition
                for (int pos = 0; pos < CoreSystem.positionList.Count; pos++)//positions
                {

                    dailyAvailShifts.Add(pos, new Dictionary<int, int>());
                    for (int i = 0; i < 7; i++)//days
                    {
                        int shifts = 0;
                        for (int k = 0; k < employeePositionDictionary[pos].Count; k++)//employee list
                        {
                            if (employeePositionDictionary[pos][k].GetAvailability(i))//isfixed
                                shifts += 1;
                        }
                        dailyAvailShifts[pos].Add(i, shifts);
                    }
                }

                for (int pos = 0; pos < CoreSystem.positionList.Count; pos++)
                {
                    weeklyAvailShifts.Add(pos, 0);
                    for (int i = 0; i < employeePositionDictionary[pos].Count; i++)
                    {
                        //Should probably review this, it makes a LOT of calls to get an int. But its fine for now
						weeklyAvailShifts[pos] += employeePositionDictionary[pos][i].hourTarget / EmployeeStorage.GetEmployee(employeePositionDictionary[pos][i].employee).shiftPreference;
                    }
                }
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("CalcPositionVars() Exception", ex);
            }
        }

        /// <summary>
        /// This will analyze data generated earlier in setup to decide if enough employees are available for each day.
        /// </summary>
        private static void AnalyzeResources()
        {
            try
            {
                for (int k = 0; k < CoreSystem.positionList.Count; k++)
                {
                    if (weeklyNeededShifts[k] > weeklyAvailShifts[k])//We have less available employee shifts for the week than we need.
                    {
                        CoreSystem.ErrorCatch("Not enough Available shifts for the week! Position: " + CoreSystem.GetPositionName(k));
                        weeklyAvailabilityStatus.Add(k, false);
                    }
                    else
                        weeklyAvailabilityStatus.Add(k, true);
                    dailyAvailabilityStatus.Add(k, new Dictionary<int, bool>());
                    for (int i = 0; i < 7; i++)
                    {
                        if (dailyAvailShifts[k][i] < dailyNeededShifts[k][i])
                        {
                            dailyAvailabilityStatus[k].Add(i, false);
                            CoreSystem.ErrorCatch("Not enough available shifts for day: " + (i + 1) + " Position: " + (k+1));
                        }
                        else
                            dailyAvailabilityStatus[k].Add(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("AnalyzeResources() Exception", ex);
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
                    pickedDays.Clear();//This is for the pickdays() method, to clear the list upon entering a new position
                    for (int d = 0; d < 7; d++)
                    {
                        //These lists will hold the employees assigned to each shift until they can be organized.
                        List<EmployeeScheduleWrapper> dailyEmployeeList = new List<EmployeeScheduleWrapper>();
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

                        //These ratios will be used if the day is critical
                        float weeklyCriticalRatio = 1.0f;
                        float dailyCriticalRatio = 1.0f;
                        float criticalOpen = 1.0f;
                        float criticalClose = 1.0f;
                        //These are the calculated number of employees for each shift
                        int openEmpCount = 0;
                        int closeEmpCount = 0;

                        if (weeklyAvailabilityStatus[pos])
                        {
                            //If this statement is true, the day is critical.
                            if (!dailyAvailabilityStatus[pos][dayInt])
                            {
                                dailyCriticalRatio = (float)dailyAvailShifts[pos][dayInt] / (float)dailyNeededShifts[pos][dayInt];
                                criticalOpen = (float)day.openNeededShifts[pos];
                                criticalClose = (float)day.closeNeededShifts[pos];
                                criticalOpen *= dailyCriticalRatio;
                                criticalClose *= dailyCriticalRatio;

                                //In this section we will assign the correct number of employees for each shift
                                //We will also assign the extra member to the smaller of the shifts if there is a tiebreaker.
                                //more open shifts needed
                                if (day.openNeededShifts[pos] > day.closeNeededShifts[pos])
                                {
                                    openEmpCount = (int)Math.Floor(criticalOpen);//move to lower value for larger shift.
                                    closeEmpCount = (int)Math.Ceiling(criticalClose);//round up for smaller shift.
                                }
                                //even shift count
                                else if (day.openNeededShifts[pos] == day.closeNeededShifts[pos])
                                {
                                    openEmpCount = (int)Math.Floor(criticalOpen);
                                    closeEmpCount = (int)Math.Floor(criticalClose);
                                }
                                //More close shifts needed
                                else
                                {
                                    openEmpCount = (int)Math.Ceiling(criticalOpen);//round up for smaller shift.
                                    closeEmpCount = (int)Math.Floor(criticalClose);//move to lower value for larger shift.
                                }
                            }
                            else//day is not critical, assign the number of employees to be exactly what we need.
                            {
                                openEmpCount = day.openNeededShifts[pos];
                                closeEmpCount = day.closeNeededShifts[pos];
                            }
                        }
                        else//weekly status takes precedent
                        {
                            weeklyCriticalRatio = (float)weeklyAvailShifts[pos] / (float)weeklyNeededShifts[pos];
                            criticalOpen = (float)day.openNeededShifts[pos];
                            criticalClose = (float)day.closeNeededShifts[pos];
                            criticalOpen *= weeklyCriticalRatio;
                            criticalClose *= weeklyCriticalRatio;

                            //In this section we will assign the correct number of employees for each shift
                            //We will also assign the extra member to the smaller of the shifts if there is a tiebreaker.
                            //more open shifts needed
                            if (day.openNeededShifts[pos] > day.closeNeededShifts[pos])
                            {
                                openEmpCount = (int)Math.Floor(criticalOpen);//move to lower value for larger shift.
                                closeEmpCount = (int)Math.Ceiling(criticalClose);//round up for smaller shift.
                            }
                            //even shift count
                            else if (day.openNeededShifts[pos] == day.closeNeededShifts[pos])
                            {
                                openEmpCount = (int)Math.Floor(criticalOpen);
                                closeEmpCount = (int)Math.Floor(criticalClose);
                            }
                            //More close shifts needed
                            else
                            {
                                openEmpCount = (int)Math.Ceiling(criticalOpen);//round up for smaller shift.
                                closeEmpCount = (int)Math.Floor(criticalClose);//move to lower value for larger shift.
                            }
                        }
                        //First we will assign the right number of employees to each day as best we can.
                        //This loop will run until either the correct number of employees is assigned, or until there are no more employees to be scheduled.
                        for (int i = 0; i < priorityList.Count; i++)
                        {
                            for (int j = 0; j < priorityList[i].Count; j++)
                            {
                                if (dailyEmployeeList.Count < (openEmpCount + closeEmpCount))
                                {
                                    dailyEmployeeList.Add(priorityList[i][j]);
                                }
                                else
                                    break;
                            }
                            if (dailyEmployeeList.Count >= (openEmpCount + closeEmpCount))
                                break;
                        }

                        //Next we will assign shifts. (BestworstMethod)
                        AssignShiftsBW(dailyEmployeeList, day, openEmpCount, closeEmpCount, pos);
                    }
                }
                ScheduleFill();
                CoreSystem.GenerationComplete(week);
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("GenerateSchedule() Exception", ex);
            }
        }

        /// <summary>
        /// This uses the Best-Worst Method, where the best and worst employees are put together for the sake of training.
        /// If all employees are generally of average skill the same effect will result.
        /// Example:
        /// Employees Assigned to Day: 13 || Daily Average = 6.1
        /// Skill Levels of Employees: 10,8,8,7,7,2,4,6,6,5,4,7,5
        /// W = Pick Worst, B = Pick Best
        /// Need 7 for Open Shift, 6 for close
        /// Starting with Open
        /// Add | Avg |Ineq | DAvg| Action
        /// 10  | 10  |  >  | 6.1 | W
        /// 2   | 6   |  <  | 6.1 | B
        /// 8   | 6.6 |  >  | 6.1 | W
        /// 4   | 6   |  <  | 6.1 | B
        /// 8   | 6.4 |  >  | 6.1 | W
        /// 4   | 6   |  <  | 6.1 | B
        /// 7   | 6.14|  >  | 6.1 | Done
        /// The remaining people left over should have closer skill levels to the average, making the second shift also average
        /// </summary>
        /// <param name="empList"></param>
        /// <param name="day"></param>
        private static void AssignShiftsBW(List<EmployeeScheduleWrapper> empList, DailySchedule day, int openEmpCount, int closeEmpCount, int pos)
        {
            try
            {
                //This is the daily average skill of the employees assigned to the day
                float dailyAvg = CalculateSkillAvg(empList);
                //In order to prevent certain employees from always opening we will simply randomly pick the first assigned shift.
                bool openFirst = CoreSystem.RandomBool();
                //This will sort the employee list so we have them in order of skill(worst to best 0 - N)
                Dictionary<int, EmployeeScheduleWrapper> sortedEmployees = SortList(empList);
                //These will be the lists that are returns to be processed into shifts
                List<EmployeeScheduleWrapper> openShift = new List<EmployeeScheduleWrapper>();
                List<EmployeeScheduleWrapper> closeShift = new List<EmployeeScheduleWrapper>();
                //These iterators will allow us to walk through the sorted list without repeating employees.
                int bestIterator = (empList.Count - 1);
                int worstIterator = 0;

                if (openFirst)
                {
                    openShift.Add(sortedEmployees[bestIterator]);//start by adding best employee
                    bestIterator -= 1;
                    if (empList.Count > 1)//This will prevent further scheduling if there is only one employee available.
                    {
                        for (int i = 0; i < openEmpCount - 1; i++)
                        {
                            if (CalculateSkillAvg(openShift) <= dailyAvg)//if the average is less than the daily we add the best employee
                            {
                                openShift.Add(sortedEmployees[bestIterator]);
                                bestIterator -= 1;
                            }
                            else//else the average is higher than the daily so we add the worst employee
                            {
                                openShift.Add(sortedEmployees[worstIterator]);
                                worstIterator++;
                            }
                        }
                        //Then we process the close shift, which generally will have a more average level of staff
                        closeShift.Add(sortedEmployees[bestIterator]);
                        bestIterator -= 1;
                        for (int i = 0; i < closeEmpCount - 1; i++)
                        {
                            if (CalculateSkillAvg(closeShift) <= dailyAvg)//if the average is less than the daily we add the best employee
                            {
                                closeShift.Add(sortedEmployees[bestIterator]);
                                bestIterator -= 1;
                            }
                            else//else the average is higher than the daily so we add the worst employee
                            {
                                closeShift.Add(sortedEmployees[worstIterator]);
                                worstIterator++;
                            }
                        }
                    }
                }
                else
                {
                    //Process close shift first.
                    closeShift.Add(sortedEmployees[bestIterator]);
                    bestIterator -= 1;
                    if (empList.Count > 1)
                    {
                        for (int i = 0; i < closeEmpCount - 1; i++)
                        {
                            if (CalculateSkillAvg(closeShift) <= dailyAvg)//if the average is less than the daily we add the best employee
                            {
                                closeShift.Add(sortedEmployees[bestIterator]);
                                bestIterator -= 1;
                            }
                            else//else the average is higher than the daily so we add the worst employee
                            {
                                closeShift.Add(sortedEmployees[worstIterator]);
                                worstIterator++;
                            }
                        }
                        //Then we process the open shift which will have a generally more average level of skill
                        openShift.Add(sortedEmployees[bestIterator]);//start by adding best employee
                        bestIterator -= 1;
                        for (int i = 0; i < openEmpCount - 1; i++)
                        {
                            if (CalculateSkillAvg(openShift) <= dailyAvg)//if the average is less than the daily we add the best employee
                            {
                                openShift.Add(sortedEmployees[bestIterator]);
                                bestIterator -= 1;
                            }
                            else//else the average is higher than the daily so we add the worst employee
                            {
                                openShift.Add(sortedEmployees[worstIterator]);
                                worstIterator++;
                            }
                        }
                    }
                }

                GenerateOpenShifts(openShift, day, pos);
                GenerateCloseShifts(closeShift, day, pos);
            }
            catch(Exception ex)
            {
                CoreSystem.ErrorCatch("AssignShiftsBW() Exception", ex);
            }
        }

        /// <summary>
        /// This method uses the Diversity method to schedule people
        /// </summary>
        /// <param name="empList"></param>
        /// <param name="day"></param>
        private static void AssignShiftsDM(List<EmployeeScheduleWrapper> empList, DailySchedule day, int openEmpCount, int closeEmpCount, int pos)
        {
            try
            {
                //This is the daily average skill of the employees assigned to the day
                float dailyAvg = CalculateSkillAvg(empList);
                //In order to prevent certain employees from always opening we will simply randomly pick the first assigned shift.
                bool openFirst = CoreSystem.RandomBool();
                //This will sort the employee list so we have them in order of skill(worst to best 0 - N)
                Dictionary<int, EmployeeScheduleWrapper> sortedEmployees = SortList(empList);
                //These will be the lists that are returns to be processed into shifts
                List<EmployeeScheduleWrapper> openShift = new List<EmployeeScheduleWrapper>();
                List<EmployeeScheduleWrapper> closeShift = new List<EmployeeScheduleWrapper>();
                //These iterators will allow us to walk through the sorted list without repeating employees.
                int bestIterator = (empList.Count - 1);
                int worstIterator = 0;

                if (openFirst)
                {
                    openShift.Add(sortedEmployees[bestIterator]);//start by adding best employee
                    bestIterator -= 1;
                    if (empList.Count > 1)//This will prevent further scheduling if there is only one employee available.
                    {
                        for (int i = 0; i < openEmpCount - 1; i++)
                        {
                            if (CalculateSkillAvg(openShift) <= dailyAvg)//if the average is less than the daily we add the best employee
                            {
                                openShift.Add(sortedEmployees[bestIterator]);
                                bestIterator -= 1;
                            }
                            else//else the average is higher than the daily so we add the worst employee
                            {
                                openShift.Add(sortedEmployees[worstIterator]);
                                worstIterator++;
                            }
                        }
                        //Then we process the close shift, which generally will have a more average level of staff
                        closeShift.Add(sortedEmployees[bestIterator]);
                        bestIterator -= 1;
                        for (int i = 0; i < closeEmpCount - 1; i++)
                        {
                            if (CalculateSkillAvg(closeShift) <= dailyAvg)//if the average is less than the daily we add the best employee
                            {
                                closeShift.Add(sortedEmployees[bestIterator]);
                                bestIterator -= 1;
                            }
                            else//else the average is higher than the daily so we add the worst employee
                            {
                                closeShift.Add(sortedEmployees[worstIterator]);
                                worstIterator++;
                            }
                        }
                    }
                }
                else
                {
                    //Process close shift first.
                    closeShift.Add(sortedEmployees[bestIterator]);
                    bestIterator -= 1;
                    if (empList.Count > 1)
                    {
                        for (int i = 0; i < closeEmpCount - 1; i++)
                        {
                            if (CalculateSkillAvg(closeShift) <= dailyAvg)//if the average is less than the daily we add the best employee
                            {
                                closeShift.Add(sortedEmployees[bestIterator]);
                                bestIterator -= 1;
                            }
                            else//else the average is higher than the daily so we add the worst employee
                            {
                                closeShift.Add(sortedEmployees[worstIterator]);
                                worstIterator++;
                            }
                        }
                        //Then we process the open shift which will have a generally more average level of skill
                        openShift.Add(sortedEmployees[bestIterator]);//start by adding best employee
                        bestIterator -= 1;
                        for (int i = 0; i < openEmpCount - 1; i++)
                        {
                            if (CalculateSkillAvg(openShift) <= dailyAvg)//if the average is less than the daily we add the best employee
                            {
                                openShift.Add(sortedEmployees[bestIterator]);
                                bestIterator -= 1;
                            }
                            else//else the average is higher than the daily so we add the worst employee
                            {
                                openShift.Add(sortedEmployees[worstIterator]);
                                worstIterator++;
                            }
                        }
                    }
                }

                GenerateOpenShifts(openShift, day, pos);
                GenerateCloseShifts(closeShift, day, pos);
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("AssignShiftsBW() Exception", ex);
            }
        }

        /// <summary>
        /// This method will fill the schedule with the remaining shifts.
        /// This particular iteration is for handling an underscheduled week, and getting people to their minimums
        /// </summary>
        private static void ScheduleFill()
        {
            try
            {
                for (int pos = 0; pos < CoreSystem.positionList.Count; pos++)
                {
                    //         day, count
                    Dictionary<int, int> remainingShiftsNeeded = new Dictionary<int, int>();
                    List<int> remainingShiftsList = new List<int>();
                    int totalShiftsNeeded = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        int shiftsNeeded = 0;
                        DailySchedule temp = week.SelectDay(i);
                        try
                        {
                            shiftsNeeded = ((temp.openNeededShifts[pos] - temp.openScheduledShifts[pos]) + (temp.closeNeededShifts[pos] - temp.closeScheduledShifts[pos]));
                        }
                        catch
                        {
                            if (!temp.openScheduledShifts.ContainsKey(pos) && !temp.closeScheduledShifts.ContainsKey(pos))//both null
                            {
                                shiftsNeeded = (temp.openNeededShifts[pos] + temp.closeNeededShifts[pos]);
                            }
                            else if (!temp.openScheduledShifts.ContainsKey(pos))//just open
                            {
                                shiftsNeeded = (temp.openNeededShifts[pos] + (temp.closeNeededShifts[pos] - temp.closeScheduledShifts[pos]));
                            }
                            else//just close
                            {
                                shiftsNeeded = ((temp.openNeededShifts[pos] - temp.openScheduledShifts[pos]) + temp.closeNeededShifts[pos]);
                            }
                        }
                        remainingShiftsNeeded.Add(i, shiftsNeeded);
                        remainingShiftsList.Add(shiftsNeeded);
                        totalShiftsNeeded += shiftsNeeded;
                    }
                    //We will store the index of the days we want to pick first here.
                    List<int> dayPickOrder = new List<int>();
                    dayPickOrder.Add(0);
                    bool inserted = false;
                    for (int i = 1; i < 7; i++)
                    {
                        inserted = false;
                        for (int k = 0; k < dayPickOrder.Count; k++)
                        {
                            if (remainingShiftsList[i] > remainingShiftsList[k])
                            {
                                //do nothing
                            }
                            else
                            {//We want to insert this day to be higher in the order since it needs more shifts
                                dayPickOrder.Insert(k, i);
                                inserted = true;
                                break;
                            }
                        }
                        if (!inserted)//If it was not higher than any of them add it to the end.
                            dayPickOrder.Add(remainingShiftsList[i]);
                    }

                    List<EmployeeScheduleWrapper> employeesNeedingShifts = new List<EmployeeScheduleWrapper>();
                    employeesNeedingShifts = CheckIfShiftsNeeded(employeePositionDictionary[pos], pos);

                    //end analysis section
                    //Start shift addition section.
                    while (true)//This loop will run till we run out employees needing shifts
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            DailySchedule day = week.SelectDay(dayPickOrder[i]);
                            for (int e = 0; e < employeesNeedingShifts.Count; e++)
                            {
                                //This will make sure they can work that day, and arent already scheduled that day
                                if (employeesNeedingShifts[e].GetAvailability(CoreSystem.ConvertDoWToInt(day.dayOfWeek)) && !day.shiftDictionary.ContainsKey(employeesNeedingShifts[e]))
                                {
                                    ScheduleEmployee(employeesNeedingShifts[e], day);
                                    employeesNeedingShifts.Remove(employeesNeedingShifts[e]);
                                    break;
                                }
                            }
                        }
                        //rebuild the list now that we have scheduled various people.
                        employeesNeedingShifts = CheckIfShiftsNeeded(employeePositionDictionary[pos], pos);
                        if (employeesNeedingShifts.Count < 1 || employeesNeedingShifts == null)
                            break;
                    }

                }
            }
            catch (Exception e)
            {
                CoreSystem.ErrorCatch("ScheduleFill() Exception", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="empList">This must be a list that can be modified so that the same thing isnt picked twice</param>
        /// <param name="i">The initial call should always start at 0</param>
        /// <param name="day">This is the day we are picking for</param>
        /// <returns></returns>
        /*private static EmployeeScheduleWrapper RecursionChoose(List<EmployeeScheduleWrapper> empList, int i, int day)
        {
            EmployeeScheduleWrapper temp;
            if (empList[i].GetAvailability(day))
            {
                temp = empList[i];
                empList.Remove(temp);
                return temp;
            }
            else if ((i + 1) >= empList.Count)//This means weve hit last index. (or higher which would be bad)
            {
                return null;
            }
            else//recursion
            {
                return RecursionChoose(empList, i + 1, day);
            }
        }*/

        private static List<EmployeeScheduleWrapper> CheckIfShiftsNeeded(List<EmployeeScheduleWrapper> empList, int pos)
        {
            List<EmployeeScheduleWrapper> employeesNeedingShifts = new List<EmployeeScheduleWrapper>();
            for (int i = 0; i < empList.Count; i++)
            {
                //this means we have less scheduled hours than the amount we want to hit.
                if (empList[i].scheduledHours < empList[i].hourTarget)
                {
                    employeesNeedingShifts.Add(empList[i]);
                }
            }
            CoreSystem.ErrorCatch("CheckIfShiftsNeeded reports " + employeesNeedingShifts.Count + " " + CoreSystem.GetPositionName(pos) + "s still need shifts.");
            return employeesNeedingShifts;
        }

        /// <summary>
        /// This will generate a shift for a single employee, choosing morning or night based on what has less. (defaulting to close if they have the same number of shifts)
        /// </summary>
        /// <param name="emp">employee to be scheduled</param>
        /// <param name="day">day to schedule them</param>
        private static void ScheduleEmployee(EmployeeScheduleWrapper emp, DailySchedule day)
        {
            int pos = emp.position;
            if (day.openScheduledShifts[pos] < day.closeScheduledShifts[pos])
            {
                int shiftLength = CoreSystem.defaultShift;
                if (EmployeeStorage.GetEmployee(emp.employee).shiftPreference != shiftLength)
                    shiftLength = EmployeeStorage.GetEmployee(emp.employee).shiftPreference;//Need to add settings on how to handle shift length preferences, but for now its fine
                Shift newShift = new Shift(emp.employee, day.openTime, (day.openTime + shiftLength), day.date.DayOfWeek);
                emp.shiftList.Add(newShift);//Adding shift to the employee wrapper so we can sort shifts by employee later.
                emp.scheduledHours += shiftLength;//Adding to users total scheduled hours
                day.shiftDictionary.Add(emp, newShift);
                if (day.openScheduledShifts.ContainsKey(pos))
                    day.openScheduledShifts[pos]++;
                else
                    day.openScheduledShifts.Add(pos, 1);
            }
            else
            {
                int shiftLength = CoreSystem.defaultShift;
                if (EmployeeStorage.GetEmployee(emp.employee).shiftPreference != shiftLength)
                    shiftLength = EmployeeStorage.GetEmployee(emp.employee).shiftPreference;//Need to add settings on how to handle shift length preferences, but for now its fine
                Shift newShift = new Shift(emp.employee, day.closeTime - shiftLength, day.closeTime, day.date.DayOfWeek);
                emp.shiftList.Add(newShift);//Adding shift to the employee wrapper so we can sort shifts by employee later.
                emp.scheduledHours += shiftLength;//Adding to users total scheduled hours
                day.shiftDictionary.Add(emp, newShift);
                if (day.closeScheduledShifts.ContainsKey(pos))
                    day.closeScheduledShifts[pos]++;
                else
                    day.closeScheduledShifts.Add(pos, 1);
            }
        }

        /// <summary>
        /// This will generate a sorted list of employees by skill from lowest to highest
        /// </summary>
        /// <param name="empList"></param>
        /// <returns></returns>
        private static Dictionary<int, EmployeeScheduleWrapper> SortList(List<EmployeeScheduleWrapper> empList)
        {
            try
            {
                List<EmployeeScheduleWrapper> tempList = new List<EmployeeScheduleWrapper>(empList);

                Dictionary<int, EmployeeScheduleWrapper> returnDict = new Dictionary<int, EmployeeScheduleWrapper>();
                int currentEntry = 0;
                while (true)
                {
                    try
                    {
                        EmployeeScheduleWrapper temp = tempList[0];
                        for (int i = 1; i < tempList.Count; i++)
                        {
                            if (tempList[i].skill < temp.skill)
                                temp = tempList[i];
                        }
                        returnDict.Add(currentEntry, temp);
                        currentEntry++;
                        tempList.Remove(temp);
                        if (tempList.Count < 1)
                            break;
                    }
                    catch
                    {
                        break;
                    }
                }
                return returnDict;
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("SortList() Exception", ex);
                return null;
            }
        }

        private static void GenerateOpenShifts(List<EmployeeScheduleWrapper> empList, DailySchedule day, int pos)
        {
            try
            {
                //This is the shift as defined in system settings
                int shiftLength = CoreSystem.defaultShift;

                for (int i = 0; i < empList.Count; i++)
                {
                    if (EmployeeStorage.GetEmployee(empList[i].employee).shiftPreference != shiftLength)
                        shiftLength = EmployeeStorage.GetEmployee(empList[i].employee).shiftPreference;//Need to add settings on how to handle shift length preferences, but for now its fine
                    Shift newShift = new Shift(empList[i].employee, day.openTime, (day.openTime + shiftLength), day.date.DayOfWeek);
                    empList[i].shiftList.Add(newShift);//Adding shift to the employee wrapper so we can sort shifts by employee later.
                    empList[i].scheduledHours += shiftLength;//Adding to users total scheduled hours
                    day.shiftDictionary.Add(empList[i], newShift);
                    if (day.openScheduledShifts.ContainsKey(pos))
                        day.openScheduledShifts[pos]++;
                    else
                        day.openScheduledShifts.Add(pos, 1);
                }
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("GenerateOpenShifts() Exception", ex);
            }
        }

        private static void GenerateCloseShifts(List<EmployeeScheduleWrapper> empList, DailySchedule day, int pos)
        {
            try
            {
                //This is the shift as defined in system settings
                int shiftLength = CoreSystem.defaultShift;

                for (int i = 0; i < empList.Count; i++)
                {
                    if (EmployeeStorage.GetEmployee(empList[i].employee).shiftPreference != shiftLength)
                        shiftLength = EmployeeStorage.GetEmployee(empList[i].employee).shiftPreference;//Need to add settings on how to handle shift length preferences, but for now its fine
                    Shift newShift = new Shift(empList[i].employee, day.closeTime - shiftLength, day.closeTime, day.date.DayOfWeek);
                    //The shift is also stored on the employee so we can easier extract each employees shifts for drawing on the GUI later
                    empList[i].shiftList.Add(newShift);
                    //Adding to users total scheduled hours
                    empList[i].scheduledHours += shiftLength;
                    day.shiftDictionary.Add(empList[i], newShift);
                    if (day.closeScheduledShifts.ContainsKey(pos))
                        day.closeScheduledShifts[pos]++;
                    else
                        day.closeScheduledShifts.Add(pos, 1);
                }
            }
            catch(Exception ex)
            {
                CoreSystem.ErrorCatch("GenerateCloseShifts() Exception", ex);
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
            try
            {
                for (int i = 0; i < 7; i++)
                {
                    if (!dailyAvailabilityStatus[pos][i] && !pickedDays.Contains(i))
                    {
                        CoreSystem.ErrorCatch("PickDay() || Priority Picked: " + (i + 1));
                        pickedDays.Add(i);
                        return week.SelectDay(i);
                    }
                }
                for (int i = 0; i < 7; i++)
                {
                    if (!pickedDays.Contains(i))
                    {
                        CoreSystem.ErrorCatch("PickDay() || Picked: " + (i + 1));
                        pickedDays.Add(i);
                        return week.SelectDay(i);
                    }
                }
                CoreSystem.ErrorCatch("PickDay() Error! || Returning Null");
                return null;//This is bad though, cause then we didnt pick one at all for some reason, this shouldn't ever happen as that means we tried to pick too many days
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("PickDay() Exception", ex);
                return null;
            }
        }

        private static float CalculateSkillAvg(List<EmployeeScheduleWrapper> empList)
        {
            try
            {
                float avg = 0.0f;
                for (int i = 0; i < empList.Count; i++)
                {
                    avg += empList[i].skill;
                }
                avg /= empList.Count;
                return avg;
            }
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("CalculateSkillAvg() Exception", ex);
                return 0;
            }
        }

        /// <summary>
        /// This generates a priority list for each day based on the number of hours each employee has already been scheduled
        /// with the goal being to schedule those with less shifts first to evenly schedule employees.
        /// </summary>
        /// <param name="masterList"></param>
        /// <returns></returns>
        private static Dictionary<int, List<EmployeeScheduleWrapper>> GeneratePriorityList(List<EmployeeScheduleWrapper> masterList, int day)
        {
            try
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
            catch (Exception ex)
            {
                CoreSystem.ErrorCatch("GeneratePriorityList() Exception", ex);
                return null;
            }
        }
    }
}