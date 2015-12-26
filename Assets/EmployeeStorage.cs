using UnityEngine;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public static class EmployeeStorage
    {
        public static int employeeCount;
        public static List<Employee> employeeList = new List<Employee>();

        public EmployeeStorage() { }
    }
}