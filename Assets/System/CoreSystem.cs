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
        public static int defaultShift;

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
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                Debug.Log(ex.InnerException);
            }
        }

        public static T DeserializeEmpList<T> (T returnObject, string fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                StreamReader reader = new StreamReader(fileName);
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

        public static Dictionary<int, List<EmployeeScheduleWrapper>> GenerateSortList(List<EmployeeScheduleWrapper> masterList)
        {
            Dictionary<int, List<EmployeeScheduleWrapper>> returnDictionary = new Dictionary<int, List<EmployeeScheduleWrapper>>();

            //All categories are based on default shift
            for (int i = 0; i < 5; i++)//because 5 list categories(though this can be expanded easily)
            {
                for (int j = 0; j < masterList.Count; j++)
                {
                    if (masterList[j].scheduledHours < (CoreSystem.defaultShift * (i + 1)) && masterList[j].scheduledHours > (CoreSystem.defaultShift * i))
                    {
                        returnDictionary[i].Add(masterList[j]);
                    }
                }
            }
        }

        public static List<EmployeeScheduleWrapper> GenerateRestrictionsList(List<EmployeeScheduleWrapper> masterList, int startHour, int endHour, int day)
        {
            for (int i = 0; i < masterList.Count; i++)
            {
                //probably dont use this method, makes shit more complicated.
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
    }
}
