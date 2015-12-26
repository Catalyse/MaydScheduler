using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys.Employees
{
    public class AddEmployee : MonoBehaviour
    {
        private EmployeeStorage storage;
        public InputField name, empid;
        public Toggle solutions, experience, fulltime, parttime;
        public Toggle sun, mon, tue, wed, thu, fri, sat;
        public Text empText, idText, typeText, timeText, avText;

        public void Start()
        {
            storage = transform.root.GetComponent<EmployeeStorage>();
        }

        public void Update()
        {

        }

        public void FieldValidation()
        {
            bool validForm = true;
            if (name.text == "")
            {
                validForm = false;
            }
            if (empid.text == "")
            {
                validForm = false;
            }
            if (solutions == false && experience == false)
            {
                validForm = false;
            }
            if (fulltime == false && parttime == false)
            {
                validForm = false;
            }
            if (sun == false && mon == false && tue == false && wed == false && thu == false && fri == false && sat)
            {
                validForm = false;
            }
        }

        private void SubmitEmployee()
        {
            Employee newEmp = new Employee(name.text, int.Parse(empid.text), solutions, fulltime, sun, mon, tue, wed, thu, fri, sat);
            storage.AddEmployee(newEmp);
        }
    }
}