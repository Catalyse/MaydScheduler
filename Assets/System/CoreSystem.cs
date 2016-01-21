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
        
        public static int RandomInt(int count)
        {
            System.Random gen = new System.Random();
            int returnVal = gen.Next(count);
            return returnVal;
        }
        
        public static bool RandomBool(int maxVal)
        {
            System.Random gen = new System.Random();
            int prob = gen.Next(maxVal);
            if (prob < (maxVal/2))
                return true;
            else
                return false;
        }
    }
}
