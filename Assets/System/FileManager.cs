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
    public static class FileManager
    {
        //FILE SERIALIZATION ========================================================================================
        public static bool CheckIfFileExists(string fileName)
        {
            fileName = fileName + ".xml";
            try
            {
                StreamReader reader = new StreamReader(fileName);
                reader.Close();
                return true;
            }
            catch//filenotfound
            {
                return false;
            }
        }

        public static void SerializeFile<T>(T objectToSerialize, string fileName)
        {
            fileName = fileName + ".xml";
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
                    CoreSystem.savedFileList.Add(fileName);
            }
            catch (Exception ex)
            {
                Debug.Log("Serialization Error || CoreSystem || SerializeFile<T>");
                Debug.Log(ex.Message);
                Debug.Log(ex.InnerException);
            }
        }

        public static T DeserializeFile<T>(string fileName)
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