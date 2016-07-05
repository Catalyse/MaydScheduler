using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CoreSys.Employees;

namespace CoreSys.Employees
{
    public class EmployeeBar : MonoBehaviour
    {
        public Text eName, title, id, status;
        public Employee employee;
        public EmployeeScheduleWrapper employeeWrap;
        public Toggle barToggle;
        private int tempStatus;

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
            eName.text = emp.empLastName + ", " + emp.empFirstName;
            id.text = emp.empID.ToString();
            title.text = CoreSystem.GetPositionName(emp.position);
            if (emp.active)
            {
                if (emp.hourTarget >= 40)
                    tempStatus = 2;
                else
                    tempStatus = 1;
            }
            else
                tempStatus = 0;
            SetBarText();
        }

        public void EmployeeBarSet(EmployeeScheduleWrapper emp)
        {
            employeeWrap = emp;
            eName.text = emp.lName +", " + emp.fName;
            id.text = emp.employee.ToString();
            title.text = CoreSystem.GetPositionName(emp.position);
            if (emp.hourTarget >= 40)
                tempStatus = 2;
            else
                tempStatus = 1;
            SetBarText();
        }

        private void SetBarText()
        {
            if (tempStatus == 0)
                status.text = "Inactive";
            else if (tempStatus == 1)
                status.text = "Part Time";
            else if (tempStatus == 2)
                status.text = "Full Time";
        }
        private void SetBarColor()
        {
            //TODO
        }
    }
}