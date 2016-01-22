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
    public static class CoreSystem
    {
        private static CoreSaveType coreSave;
        private static bool systemInitialized; 
        public static int currentWeekID;
        public static int defaultShift;
        public static Dictionary<int, string> positionList = new Dictionary<int, string>();
        public static Dictionary<int, Day> dayList = new Dictionary<int, Day>();
        public static List<string> savedFileList = new List<string>();

        public static void LoadCoreSave()
        {
            coreSave = new CoreSaveType();
            coreSave = DeserializeFile<CoreSaveType>("CoreSaveFile");
        }

        private static void SystemInitialization()
        {
            //use this to find the first day of the week of the year
            int yearLength = 365;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            DayOfWeek yearStart = dt.DayOfWeek;
            int startDay = (int)yearStart;
            if (DateTime.IsLeapYear(dt.Year))
                yearLength = 366;
            for (int i = 0; i < yearLength; i++)
            {

            }
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
                return "Error!";
            }
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

        //FILE SERIALIZATION ========================================================================================
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
