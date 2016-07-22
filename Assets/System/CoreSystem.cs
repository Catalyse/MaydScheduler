using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Errors;
using CoreSys.Windows;
using System.Threading;
using CoreSys.Employees;

namespace CoreSys
{
    struct RestrictedReturn
    {
        public bool canWork;
        public int maxShift;
    }
    
    /// <summary>
    /// This class serves as the central control point for the program
    /// Or reference point rather since it doesnt really have much direct control
    /// </summary>
    public static class CoreSystem
    {
        public static CoreSettingsType coreSettings;
        public static CoreSaveType coreSave;
        public static bool coreSettingsLoaded = false;
        public static bool coreSaveLoaded = false;
        //public static bool systemInitialized; 
        //public static int currentWeekID;
        public static int defaultShift, minShift, maxShift;//Need to make config window
        public static int defaultOpenAvail, defaultCloseAvail;
        public static float skillLevelCap;
        public static SerializableDictionary<int, string> positionList = new SerializableDictionary<int, string>();
        public static SerializableDictionary<DateTime, Week> weekList = new SerializableDictionary<DateTime, Week>();
        public static List<string> savedFileList = new List<string>();
        public static string GenerationDate;

        //This section is for thread management of the scheduling algorithm
        public static Week week;
        public static bool currentlyProcessing = false;//This is for unity update loops to check if the subthread for the scheudle is still running.
        public static bool loadingCoreSave = false;

        public static void DebugStop()
        {
            Debug.Log("Debug Stop!");//Throw debug marker here
        }

        /// <summary>
        /// This method is used to load default system settings from file
        /// </summary>
        public static void LoadCoreSettings()
        {
            if (FileManager.CheckIfFileExists("CoreSettings"))
            {
                coreSettings = new CoreSettingsType();
                coreSettings = FileManager.DeserializeFile<CoreSettingsType>("CoreSettings");
                defaultShift = coreSettings.defaultShift;
                defaultOpenAvail = coreSettings.defaultOpenAvail;
                defaultCloseAvail = coreSettings.defaultCloseAvail;
                minShift = coreSettings.minShift;
                maxShift = coreSettings.maxShift;
                skillLevelCap = coreSettings.skillLevelCap;
                positionList = coreSettings.positionList;
                savedFileList = coreSettings.savedFileList;
                GenerationDate = DateTime.Now.ToString();
                coreSettingsLoaded = true;
                Debug.Log("Core Settings File Loaded!");
            }
            else
            {
                coreSettingsLoaded = false;
                Debug.Log("Core Settings File not found!");
                coreSettings = new CoreSettingsType();
                coreSettings.GenerationDate = DateTime.Now.ToString();//THis sets the gen date, although if a save is never completed on a weeklist then the gen date wont be saved
            }
        }

        public static void CoreSettingsChanged()
        {
            coreSettings = new CoreSettingsType();
            coreSettings.defaultShift = defaultShift;
            coreSettings.defaultOpenAvail = defaultOpenAvail;
            coreSettings.defaultCloseAvail = defaultCloseAvail;
            coreSettings.minShift = minShift;
            coreSettings.maxShift = maxShift;
            coreSettings.skillLevelCap = skillLevelCap;
            coreSettings.positionList = positionList;
            coreSettings.savedFileList = savedFileList;
            coreSettings.LastModified = DateTime.Now.ToString();
            FileManager.SerializeFile<CoreSettingsType>(coreSettings, "CoreSettings");
            Debug.Log("Core Settings File Modified! Core Settings File Saved!");
        }

        public static void LoadCoreSave()
        {
            if (FileManager.CheckIfFileExists("CoreSave"))
            {
                coreSave = new CoreSaveType();
                FileManager.DeserializeCoreSave("CoreSettings");
                coreSaveLoaded = true;
            }
            else
            {
                coreSaveLoaded = false;
                Debug.Log("Core Save File not found!");
                coreSave = new CoreSaveType();
                coreSave.GenerationDate = DateTime.Now.ToString(); //THis sets the gen date, although if a save is never completed on a weeklist then the gen date wont be saved
            }
        }

        public static void CoreSaveLoaded(CoreSaveType save)//This exists because the load is threaded
        {
            coreSave = save;
            coreSaveLoaded = true;
            weekList = coreSave.weekList;
        }

        public static void CoreSaveChanged()
        {
            if (coreSave != null)
            {
                coreSave.weekList = weekList;
                coreSave.LastModified = DateTime.Now.ToString();
                FileManager.SerializeCoreSave();
            }
            else
            {
                coreSave = new CoreSaveType();
                coreSave.weekList = weekList;
                coreSave.LastModified = DateTime.Now.ToString();
                FileManager.SerializeCoreSave();
            }
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
                Debug.Log("Position that does not exist was queried for! || CoreSystem.cs || GetPositionName || TypeNo: " + type);
                return "ErrNotFound";
                //TODO, make this force a popup to define the queried type
            }
        }

        /// <summary>
        /// Method to begin thread for schedule generation
        /// Also generates wrapper list due to temp availability
        /// </summary>
        /// <param name="w"></param>
        public static void GenerateSchedule(Week w)
        {
            Debug.Log("Starting Schedule Generation");
            currentlyProcessing = true;//This allows instanced objects to track whether the threaded generation is done or not
            week = w;
            Thread scheduleProcess = new Thread(new ThreadStart(SchedulingAlgorithm.StartScheduleGen));
            scheduleProcess.Start();
        }

        public static void GenerationComplete(Week w)
        {
            week = w;
            currentlyProcessing = false;
            if (weekList.ContainsKey(w.startDate))//This will overwrite the week in the event that it already existed and we are regenerating it.
            {
                weekList.Remove(w.startDate);
                weekList.Add(w.startDate, w);
            }
            else
            {
                weekList.Add(w.startDate, w);
            }
            CoreSaveChanged();
        }

        //Random Common Methods ======================================================================================
        public static void ErrorCatch(string msg)
        {
            Debug.Log(msg);
            SystemMessages.ThrowMessage(msg);
        }

        public static void ErrorCatch(string msg, Exception ex)
        {
            Debug.Log(msg);
            SystemMessages.ThrowMessage(msg);
            Debug.Log(ex.Message + "\n" + ex.InnerException + "\n" + ex.StackTrace);
        }

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

        public static int ConvertDoWToInt(DayOfWeek d)
        {
            switch (d)
            {
                case DayOfWeek.Sunday:
                    return 0;
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    Debug.Log("ConvertDoWToInt() Error || Invalid DayOfWeek provided");
                    return -1;
            }
        }
    }
}
