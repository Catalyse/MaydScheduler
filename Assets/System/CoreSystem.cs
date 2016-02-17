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
        public static Dictionary<int, string> positionList = new Dictionary<int, string>();
        //public static Dictionary<int, DailySchedule> dayList = new Dictionary<int, DailySchedule>();
        //public static Dictionary<int, Week> weekList = new Dictionary<int, Week>();
        public static Dictionary<DateTime, Week> weekList = new Dictionary<DateTime, Week>();//uses a datetime to define a week, beginning with Sunday the start date
        public static List<string> savedFileList = new List<string>();
        public static string GenerationDate;

        public static void DebugStop()
        {
            Debug.Log("Debug Stop!");//Throw debug marker here
        }

        public static void LoadCoreSave()
        {
            if (CheckIfFileExists("CoreSaveFile"))
            {
                coreSave = new CoreSaveType();
                coreSave = DeserializeFile<CoreSaveType>("CoreSaveFile");
                coreSaveLoaded = true;
            }
            else
            {
                coreSaveLoaded = false;
            }
        }

        public static Week FindWeek(DateTime weekStartDate)
        {
            if(weekList.ContainsKey(weekStartDate))
                return weekList[weekStartDate];
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
