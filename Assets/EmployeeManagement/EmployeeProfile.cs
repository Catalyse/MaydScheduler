using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Employees
{
    public class EmployeeProfile : Window
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
            empName.text = emp.empLastName;
            if (emp.solutions)
                empTitle.text = "Solutions Specialist";
            else
                empTitle.text = "Experience Specialist";

        }

        public void FillStartEndVals()
        {
            if (sun.isOn)
            {
                
            }
            else if (mon.isOn)
            {

            }
            else if (tue.isOn)
            {

            }
            else if (wed.isOn)
            {

            }
            else if (thu.isOn)
            {

            }
            else if (fri.isOn)
            {

            }
            else if (sat.isOn)
            {

            }
        }
        /// <summary>
        ///  When the submit button is hit, this method is called.
        ///  When called the method figures out which day you are working on, then sets that days values.
        /// </summary>
        public void SetAvailability()
        {
            if (sun.isOn)
            {

            }
            else if (mon.isOn)
            {

            }
            else if (tue.isOn)
            {

            }
            else if (wed.isOn)
            {

            }
            else if (thu.isOn)
            {

            }
            else if (fri.isOn)
            {

            }
            else if (sat.isOn)
            {

            }
        }
    }
}