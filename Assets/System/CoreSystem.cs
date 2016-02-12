using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Errors;

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
        public static bool systemInitialized; 
        public static int currentWeekID;
        public static int defaultShift, minShift;
        public static float skillLevelCap;
        public static Dictionary<int, string> positionList = new Dictionary<int, string>();
        public static Dictionary<int, DailySchedule> dayList = new Dictionary<int, DailySchedule>();
        public static Dictionary<int, Week> weekList = new Dictionary<int, Week>();
        public static List<string> savedFileList = new List<string>();
        public static string GenerationDate;

        public static void DebugStop()
        {
            Debug.Log("Debug Stop!");//Throw debug marker here
        }

        public static void LoadCoreSave()
        {
            coreSave = new CoreSaveType();
            coreSave = DeserializeFile<CoreSaveType>("CoreSaveFile");
        }

        private static void SystemInitialization()
        {
            //use this to find the first day of the week of the year
            int yearLength = 365;
            defaultShift = 8;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            DayOfWeek yearStart = dt.DayOfWeek;
            int startDay = (int)yearStart;
            if (DateTime.IsLeapYear(DateTime.Now.Year))
                yearLength = 366;
            for (int i = 1; i < yearLength; i++)
            {
                //                          TODO FIX THIS SHIT (Cause hardcoding shit is bad mkay)
                DateTime date = new DateTime(DateTime.Now.Year, 1, 1).AddDays(i - 1);
                DailySchedule newDay = new DailySchedule(date);
                dayList.Add(i, newDay);
            }

            int startDayOfWeek = (int)dt.DayOfWeek;
            int firstWeekDate = 7 - startDayOfWeek;
            int j = firstWeekDate;
            int k = 0;//Keep track of which week we are in (from 0 to 51)

            for (; j < yearLength; j += 7)
            {
                Week newWeek = new Week(j);
                weekList.Add(k, newWeek);
                k++;
            }

            GenerationDate = DateTime.Now.ToString();
        }

        public static int GenerateWeekID()
        {
            int returnID = currentWeekID;
            currentWeekID++;
            return returnID;
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
                return "Error! Contact Dev";
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

        //FILE SERIALIZATION ========================================================================================
        public static bool CheckIfFileExists(string fileName)
        {
            if(savedFileList.Contains(fileName))
                return true;
            else
                return false;
        }

        public static void SerializeFile<T>(T objectToSerialize, string fileName)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(objectToSerialize.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, objectToSerialize);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
                if (fileName != "CoreSaveFile")
                    savedFileList.Add(fileName);
            }
            catch (Exception ex)
            {
                Debug.Log("Serialization Error || CoreSystem || SerializeFile<T>");
                Debug.Log(ex.Message);
                Debug.Log(ex.InnerException);
            }
        }

        public static T DeserializeFile<T> (string fileName)
        {
            string file = fileName + ".xml";
            T returnObject;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                StreamReader reader = new StreamReader(file);
                if (reader == null)
                    throw new EmpListNotFoundErr();
                returnObject = (T)serializer.Deserialize(reader);
                reader.Close();

                return returnObject;
            }
            catch (Exception ex)
            {
                Debug.Log("FileNotFound Exception!");
                Debug.Log(ex.Message);
                return default(T);
            }
        }
        //FILE SERIALIZATION ========================================================================================
    }
}
