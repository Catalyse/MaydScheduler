using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CoreSys.Employees;

namespace CoreSys.Employees
{
    public class EmployeeBar : MonoBehaviour
    {
        public Text BarText;
        public Employee employee;
        private string empName, title, statustext;
        private int id, status;
        public Toggle barToggle;

        public EmployeeBar() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">empname</param>
        /// <param name="id">empid</param>
        /// <param name="title"></param>
        /// <param name="status">0 for inactive, 1 for part time, 2 for fulltime</param>
        public void EmployeeBarSet(Employee emp)
        {
            employee = emp;
            empName = emp.empFirstName + emp.empLastName;
            id = emp.empID;
            title = CoreSystem.GetPositionName(emp.position);
            if (emp.active)
            {
                if (emp.hourTarget >= 40)
                    status = 2;
                else
                    status = 1;
            }
            else
                status = 0;
            SetBarText();
        }

        private void SetBarText()
        {
            if (status == 0)
                statustext = "Inactive";
            else if (status == 1)
                statustext = "Part Time";
            else if (status == 2)
                statustext = "Full Time";
            BarText.text = empName + " || " + title + " || " + id + " || " + statustext;
        }
        private void SetBarColor()
        {
            //TODO
        }
    }
}