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
                

                XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));

                StreamReader reader = new StreamReader(fileName);
                if (reader == null)
                    throw new EmpListNotFoundErr();
                storage = (List<Employee>)serializer.Deserialize(reader);
                reader.Close();

                return storage;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return null;
            }
        }
    }
}