using UnityEngine;
using System;
using System.Collections.Generic;

namespace CoreSys
{

    //Review This File
    //It may be pertinent to merge this file with the scheduler algorithm

    public static class CoreSystem
    {
        //create employeeschedulewrapper for each employee then we can keep track of each employees total hours
        public static Dictionary<int, List<EmployeeScheduleWrapper>> GenerateSortList(List<EmployeeScheduleWrapper> masterList)
        {
            Dictionary<int, List<EmployeeScheduleWrapper>> returnDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();
            
            //All categories are based on default shift
            for(int i = 0; i < 5; i++)//because 5 list categories(though this can be expanded easily)
            {
                for(int j = 0; j < masterList.Count; j++)
                {
                    if(masterList[j].scheduledHours < (CoreSystem.defaultShift * (i+1)) && masterList[j].scheduledHours > (CoreSystem.defaultShift * i))
                    {
                        returnDictionary[i].Add(masterList[j]);
                    }
                }
            }
        }
        
        public static List<EmployeeScheduleWrapper> GenerateRestrictionsList(List<EmployeeScheduleWrapper> masterList, int startHour, int endHour, int day)
        {
            for(int i = 0; i < masterList.Count; i++)
            {
                //probably dont use this method, makes shit more complicated.
            }
        }
    }
}
