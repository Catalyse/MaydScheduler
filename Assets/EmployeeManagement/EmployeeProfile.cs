using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Employees
{
    public class EmployeeProfile : MonoBehaviour
    {
        private Employee employee;
        public Text empName, empTitle, empID, empTenure, empStatus;
        public InputField start, end;
        public Toggle sun, mon, tue, wed, thu, fri, sat;
        public Toggle canwork;

        public EmployeeProfile() { }

        /// <summary>
        /// After the window is instantiated this function must be called to fill in the profile window
        /// </summary>
        /// <param name="emp">This is the employee whos profile is being displayed</param>
        public void SetProfile(Employee emp)
        {
            employee = emp;
            empName.text = emp.name;
            if (emp.solutions)
                empTitle.text = "Solutions Specialist";
            else
                empTitle.text = "Experience Specialist";

        }

        public void FillStartEndVals()
        {
            if (sun)
            {
                
            }
            else if (mon)
            {

            }
            else if (tue)
            {

            }
            else if (wed)
            {

            }
            else if (thu)
            {

            }
            else if (fri)
            {

            }
            else if (sat)
            {

            }
        }
        /// <summary>
        ///  When the submit button is hit, this method is called.
        ///  When called the method figures out which day you are working on, then sets that days values.
        /// </summary>
        public void SetAvailability()
        {
            if (sun)
            {

            }
            else if (mon)
            {

            }
            else if (tue)
            {

            }
            else if (wed)
            {

            }
            else if (thu)
            {

            }
            else if (fri)
            {

            }
            else if (sat)
            {

            }
        }
    }
}