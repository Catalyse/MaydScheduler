using UnityEngine;
using UnityEngine.UI;
using System;
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
            empName.text = EmployeeStorage.GetEmployee(emp.employee).empLastName + ", " + EmployeeStorage.GetEmployee(emp.employee).empFirstName;
            empPos.text = CoreSystem.GetPositionName(emp.position);
            for (int i = 0; i < emp.shiftList.Count; i++)
            {
                switch (emp.shiftList[i].date)
                {
                    case DayOfWeek.Sunday:
                        sun.text = emp.shiftList[i].startShift.ToString() + "00 - " + emp.shiftList[i].endShift.ToString() + "00";
                        break;
                    case DayOfWeek.Monday:
                        mon.text = emp.shiftList[i].startShift.ToString() + "00 - " + emp.shiftList[i].endShift.ToString() + "00";
                        break;
                    case DayOfWeek.Tuesday:
                        tue.text = emp.shiftList[i].startShift.ToString() + "00 - " + emp.shiftList[i].endShift.ToString() + "00";
                        break;
                    case DayOfWeek.Wednesday:
                        wed.text = emp.shiftList[i].startShift.ToString() + "00 - " + emp.shiftList[i].endShift.ToString() + "00";
                        break;
                    case DayOfWeek.Thursday:
                        thu.text = emp.shiftList[i].startShift.ToString() + "00 - " + emp.shiftList[i].endShift.ToString() + "00";
                        break;
                    case DayOfWeek.Friday:
                        fri.text = emp.shiftList[i].startShift.ToString() + "00 - " + emp.shiftList[i].endShift.ToString() + "00";
                        break;
                    case DayOfWeek.Saturday:
                        sat.text = emp.shiftList[i].startShift.ToString() + "00 - " + emp.shiftList[i].endShift.ToString() + "00";
                        break;
                }
            }
        }
    }
}