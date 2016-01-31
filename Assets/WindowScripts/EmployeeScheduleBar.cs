using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CoreSys;
using CoreSys.Employees;

namespace CoreSys.Windows
{
    public class EmployeeScheduleBar : MonoBehaviour
    {
        public Text empName, empPos;
        public Text sun, mon, tue, wed, thu, fri, sat;
        //Set an edit button to pop up some shit

        public EmployeeScheduleBar() { }

        public void SetBar(EmployeeScheduleWrapper emp)
        {

        }
    }
}