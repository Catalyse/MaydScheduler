using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Employees
{
    public class EmployeeProfile : MonoBehaviour
    {
        public Text empName, empTitle, empID, empTenure, empStatus;
        public InputField suStart, mStart, tuStart, wStart, thStart, fStart, saStart, suEnd, mEnd, tuEnd, wEnd, thEnd, fEnd, saEnd;
        public Toggle sun, mon, tue, wed, thu, fri, sat;
        public Toggle suCanWork, mCanWork, tuCanWork, wCanWork, thCanWork, fCanWork, saCanWork;
    }
}