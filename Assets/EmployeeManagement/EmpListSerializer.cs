using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using CoreSys.Errors;

namespace CoreSys
{
    public static class EmpListSerializer
    {
        public static string fileName = "empList.xml";

        public static void SerializeObject(EmployeeStorage list)
        {
            if (list == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(list.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, list);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Data);
            }
        }


        public static EmployeeStorage DeserializeEmpList()
        {
            try
            {
                EmployeeStorage storage = null;

                XmlSerializer serializer = new XmlSerializer(typeof(EmployeeStorage));

                StreamReader reader = new StreamReader(fileName);
                if (reader == null)
                    throw new EmpListNotFoundErr();
                storage = (EmployeeStorage)serializer.Deserialize(reader);
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