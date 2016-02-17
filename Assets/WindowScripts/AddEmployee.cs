using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys.Employees
{
    public class AddEmployee : Window
    {
        public EmployeeManagement empManager;
        public InputField empFirstName, empLastName, empid, skill;
        public Toggle fulltime, parttime;
        public Toggle sun, mon, tue, wed, thu, fri, sat;
        public Text empErrLastText, empErrFirstText, idErrText, typeErrText, timeErrText, avErrText, skillPlaceholder, skillErrText;
        public Dropdown dropdown;

        public void Update()
        {
            ErrorTextFade();
        }

        public void WindowEnabled()
        {
            ConfigDropdown();
            ClearAllFields();
        }

        private void ErrorTextFade()
        {
            if (empErrFirstText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(empErrFirstText.color.a, 0, 2 * Time.deltaTime);
                empErrFirstText.color = new Color(empErrFirstText.color.r, empErrFirstText.color.g, empErrFirstText.color.b, newAlpha);
            }
            else
                empErrFirstText.enabled = false;
            if (empErrLastText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(empErrLastText.color.a, 0, 2 * Time.deltaTime);
                empErrLastText.color = new Color(empErrLastText.color.r, empErrLastText.color.g, empErrLastText.color.b, newAlpha);
            }
            else
                empErrLastText.enabled = false;
            if (idErrText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(idErrText.color.a, 0, 2 * Time.deltaTime);
                idErrText.color = new Color(idErrText.color.r, idErrText.color.g, idErrText.color.b, newAlpha);
            }
            else
                idErrText.enabled = false;
            if (typeErrText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(typeErrText.color.a, 0, 2 * Time.deltaTime);
                typeErrText.color = new Color(typeErrText.color.r, typeErrText.color.g, typeErrText.color.b, newAlpha);
            }
            else
                typeErrText.enabled = false;
            if (timeErrText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(timeErrText.color.a, 0, 2 * Time.deltaTime);
                timeErrText.color = new Color(timeErrText.color.r, timeErrText.color.g, timeErrText.color.b, newAlpha);
            }
            else
                timeErrText.enabled = false;
            if (avErrText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(avErrText.color.a, 0, 2 * Time.deltaTime);
                avErrText.color = new Color(avErrText.color.r, avErrText.color.g, avErrText.color.b, newAlpha);
            }
            else
                avErrText.enabled = false;
        }

        public void FieldValidation()
        {
            bool validForm = true;
            if (empLastName.text == "")
            {
                empErrLastText.enabled = true;
                empErrLastText.color = new Color(empErrLastText.color.r, empErrLastText.color.g, empErrLastText.color.b, 50);
                validForm = false;
            }
            if (empFirstName.text == "")
            {
                empErrFirstText.enabled = true;
                empErrFirstText.color = new Color(empErrFirstText.color.r, empErrFirstText.color.g, empErrFirstText.color.b, 50);
                validForm = false;
            }
            if (empid.text == "")
            {
                idErrText.enabled = true;
                idErrText.color = new Color(idErrText.color.r, idErrText.color.g, idErrText.color.b, 50);
                validForm = false;
            }/*
            if (solutions.isOn == false && experience.isOn == false)
            {
                typeErrText.enabled = true;
                typeErrText.color = new Color(typeErrText.color.r, typeErrText.color.g, typeErrText.color.b, 50);
                validForm = false;
            }*///I think I need to do something with this
            if (fulltime.isOn == false && parttime.isOn == false)
            {
                timeErrText.enabled = true;
                timeErrText.color = new Color(timeErrText.color.r, timeErrText.color.g, timeErrText.color.b, 50);
                validForm = false;
            }
            if (sun.isOn == false && mon.isOn == false && tue.isOn == false && wed.isOn == false && thu.isOn == false && fri.isOn == false && sat.isOn == false)
            {
                avErrText.enabled = true;
                avErrText.color = new Color(avErrText.color.r, avErrText.color.g, avErrText.color.b, 50);
                validForm = false;
            }
            if (validForm == true)
                SubmitEmployee();
        }

        public void ConfigDropdown()
        {
            dropdown.ClearOptions();
            List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData>();
            for (int i = 0; i < CoreSystem.positionList.Count; i++)
            {
                Dropdown.OptionData newOption = new Dropdown.OptionData(CoreSystem.positionList[i]);
                optionList.Add(newOption);
            }

            dropdown.AddOptions(optionList);
        }

        private void SubmitEmployee()
        {
            Employee newEmp = new Employee();
            int targetHours;
            if (fulltime)
                targetHours = 40;
            else
                targetHours = 25;
            newEmp.SetEmployee(empLastName.text, empFirstName.text, int.Parse(empid.text), dropdown.value, targetHours, int.Parse(skill.text), sun, mon, tue, wed, thu, fri, sat);
            ClearAllFields();
            empManager.AddEmployee(newEmp);
        }

        private void ClearAllFields()
        {
            empFirstName.text = "";
            empLastName.text = "";
            empid.text = "";
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