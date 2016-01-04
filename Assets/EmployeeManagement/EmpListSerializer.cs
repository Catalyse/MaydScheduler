using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Errors;

namespace CoreSys.Employees
{
    public static class EmpListSerializer
    {
        public static string fileName = "empList.xml";

        public static void SerializeEmpList(List<Employee> list)
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
                Debug.Log(ex.Message);
                Debug.Log(ex.InnerException);
            }
        }


        public static List<Employee> DeserializeEmpList()
        {
            try
            {
                List<Employee> storage = new List<Employee>();

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