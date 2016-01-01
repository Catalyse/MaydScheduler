using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Employees
{
    public class EmployeeProfile : MonoBehaviour
    {
        public Text empName, empTitle, empID, empTenure, empStatus;
        public InputField start, end;
        public Toggle sun, mon, tue, wed, thu, fri, sat;
        public Toggle canwork;
    }
}