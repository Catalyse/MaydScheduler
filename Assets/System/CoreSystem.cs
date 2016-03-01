using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Errors;
using CoreSys.Windows;

namespace CoreSys
{
    struct RestrictedReturn
    {
        public bool canWork;
        public int maxShift;
    }
    
    /// <summary>
    /// This class serves as the central control point for the program
    /// </summary>
    public static class CoreSystem
    {
        private static CoreSaveType coreSave;
        public static bool coreSaveLoaded = false;
        //public static bool systemInitialized; 
        //public static int currentWeekID;
        public static int defaultShift, minShift, maxShift;//Need to make config window
        public static int defaultOpenAvail, defaultCloseAvail;
        public static float skillLevelCap;
        public static SerializableDictionary<int, string> positionList = new SerializableDictionary<int, string>();
        //public static Dictionary<int, DailySchedule> dayList = new Dictionary<int, DailySchedule>();
        //public static Dictionary<int, Week> weekList = new Dictionary<int, Week>();
        public static SerializableDictionary<DateTime, Week> weekList = new SerializableDictionary<DateTime, Week>();
        public static List<string> savedFileList = new List<string>();
        public static string GenerationDate;

        public static void DebugStop()
        {
            Debug.Log("Debug Stop!");//Throw debug marker here
        }

        /// <summary>
        /// This method is used to load default system settings from file
        /// </summary>
        public static void LoadCoreSave()
        {
            if (FileManager.CheckIfFileExists("CoreSaveFile"))
            {
                coreSave = new CoreSaveType();
                coreSave = FileManager.DeserializeFile<CoreSaveType>("CoreSaveFile");
                defaultShift = coreSave.defaultShift;
                defaultOpenAvail = coreSave.defaultOpenAvail;
                defaultCloseAvail = coreSave.defaultCloseAvail;
                minShift = coreSave.minShift;
                maxShift = coreSave.maxShift;
                skillLevelCap = coreSave.skillLevelCap;
                positionList = coreSave.positionList;
                weekList = coreSave.weekList;
                savedFileList = coreSave.savedFileList;
                GenerationDate = DateTime.Now.ToString();
                coreSaveLoaded = true;
                Debug.Log("CoreSave File Loaded!");
            }
            else
            {
                coreSaveLoaded = false;
                Debug.Log("CoreSave File not found!");
            }
        }

        public static void CoreSettingsChanged()
        {
            coreSave = new CoreSaveType();
            coreSave.defaultShift = defaultShift;
            coreSave.defaultOpenAvail = defaultOpenAvail;
            coreSave.defaultCloseAvail = defaultCloseAvail;
            coreSave.minShift = minShift;
            coreSave.maxShift = maxShift;
            coreSave.skillLevelCap = skillLevelCap;
            coreSave.positionList = positionList;
            coreSave.weekList = weekList;
            coreSave.savedFileList = savedFileList;
            coreSave.GenerationDate = DateTime.Now.ToString();
            FileManager.SerializeFile<CoreSaveType>(coreSave, "CoreSaveFile");
            Debug.Log("CoreSave File Modified! CoreSave File Saved!");
        }

        public static Week FindWeek(DateTime weekStartDate)
        {
            if(weekList.ContainsKey(weekStartDate.Date))
                return weekList[weekStartDate.Date];
            else
                return null;
        }

        public static string GetPositionName(int type)
        {
            if (positionList.ContainsKey(type))
            {
                return positionList[type];
            }
            else
            {
                Debug.Log("Position that does not exist was queried for! || CoreSystem.cs || GetPositionName");
                return "Error! Contact Developer!";
                //TODO, make this force a popup to define the queried type
            }
        }

        //Random Common Methods ======================================================================================
        public static int RandomInt(int count)
        {
            System.Random gen = new System.Random();
            int returnVal = gen.Next(count);
            return returnVal;
        }

        public static bool RandomBool()
        {
            System.Random gen = new System.Random();
            int prob = gen.Next(100);
            if (prob < 50)
                return true;
            else
                return false;
        }

        public static int ConvertToMilitaryTime(int time)
        {
            if (time <= 12)
            {
                time += 12;
                return time;
            }
            else
                return time;
        }
    }
}
