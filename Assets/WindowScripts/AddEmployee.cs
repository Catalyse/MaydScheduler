using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys.Employees
{
    public class AddEmployee : Window
    {
        public EmployeeManagement empManager;
        public InputField empFirstName, empLastName, empid;
        public Toggle solutions, experience, fulltime, parttime;
        public Toggle sun, mon, tue, wed, thu, fri, sat;
        public Text empLastText, empFirstText, idText, typeText, timeText, avText;

        public void Update()
        {
            ErrorTextFade();
        }

        private void ErrorTextFade()
        {
            if (empFirstText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(empFirstText.color.a, 0, 2 * Time.deltaTime);
                empFirstText.color = new Color(empFirstText.color.r, empFirstText.color.g, empFirstText.color.b, newAlpha);
            }
            else
                empFirstText.enabled = false;
            if (empLastText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(empLastText.color.a, 0, 2 * Time.deltaTime);
                empLastText.color = new Color(empLastText.color.r, empLastText.color.g, empLastText.color.b, newAlpha);
            }
            else
                empLastText.enabled = false;
            if (idText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(idText.color.a, 0, 2 * Time.deltaTime);
                idText.color = new Color(idText.color.r, idText.color.g, idText.color.b, newAlpha);
            }
            else
                idText.enabled = false;
            if (typeText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(typeText.color.a, 0, 2 * Time.deltaTime);
                typeText.color = new Color(typeText.color.r, typeText.color.g, typeText.color.b, newAlpha);
            }
            else
                typeText.enabled = false;
            if (timeText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(timeText.color.a, 0, 2 * Time.deltaTime);
                timeText.color = new Color(timeText.color.r, timeText.color.g, timeText.color.b, newAlpha);
            }
            else
                timeText.enabled = false;
            if (avText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(avText.color.a, 0, 2 * Time.deltaTime);
                avText.color = new Color(avText.color.r, avText.color.g, avText.color.b, newAlpha);
            }
            else
                avText.enabled = false;
        }

        public void FieldValidation()
        {
            bool validForm = true;
            if (empLastName.text == "")
            {
                empLastText.enabled = true;
                empLastText.color = new Color(empLastText.color.r, empLastText.color.g, empLastText.color.b, 50);
                validForm = false;
            }
            if (empFirstName.text == "")
            {
                empFirstText.enabled = true;
                empFirstText.color = new Color(empFirstText.color.r, empFirstText.color.g, empFirstText.color.b, 50);
                validForm = false;
            }
            if (empid.text == "")
            {
                idText.enabled = true;
                idText.color = new Color(idText.color.r, idText.color.g, idText.color.b, 50);
                validForm = false;
            }
            if (solutions.isOn == false && experience.isOn == false)
            {
                typeText.enabled = true;
                typeText.color = new Color(typeText.color.r, typeText.color.g, typeText.color.b, 50);
                validForm = false;
            }
            if (fulltime.isOn == false && parttime.isOn == false)
            {
                timeText.enabled = true;
                timeText.color = new Color(timeText.color.r, timeText.color.g, timeText.color.b, 50);
                validForm = false;
            }
            if (sun.isOn == false && mon.isOn == false && tue.isOn == false && wed.isOn == false && thu.isOn == false && fri.isOn == false && sat.isOn == false)
            {
                avText.enabled = true;
                avText.color = new Color(avText.color.r, avText.color.g, avText.color.b, 50);
                validForm = false;
            }
            if (validForm == true)
                SubmitEmployee();
        }

        private void SubmitEmployee()
        {
            Employee newEmp = new Employee();
            int position, targetHours;
            if(solutions) //TODO this window needs to be fixed and show available positions with a dropdown
                position = 0;
            else
                position = 1;
            if (fulltime)
                targetHours = 40;
            else
                targetHours = 25;
            newEmp.SetEmployee(empLastName.text, empFirstName.text, int.Parse(empid.text), position, targetHours, sun, mon, tue, wed, thu, fri, sat);
            ClearAllFields();
            empManager.AddEmployee(newEmp);
        }

        private void ClearAllFields()
        {
            empFirstName.text = "";
            empLastName.text = "";
            empid.text = "";
            solutions.isOn = false;
            experience.isOn = false;
            fulltime.isOn = false;
            parttime.isOn = false;
            sun.isOn = false;
            mon.isOn = false;
            tue.isOn = false;
            wed.isOn = false;
            thu.isOn = false;
            fri.isOn = false;
            sat.isOn = false;
        }
    }
}