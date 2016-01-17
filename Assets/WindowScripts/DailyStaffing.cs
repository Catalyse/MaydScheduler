using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    /// <summary>
    /// Daily Staffing requirements window script.
    /// </summary>
    public class DailyStaffing : Window
    {
        public InputField sundayOpen, mondayOpen, tuesdayOpen, wednesdayOpen, thursdayOpen, fridayOpen, saturdayOpen;
        public InputField sundayClose, mondayClose, tuesdayClose, wednesdayClose, thursdayClose, fridayClose, saturdayClose;
        public GameObject sunday, monday, tuesday, wednesday, thursday, friday, saturday;
        public Text suOpenText, moOpenText, tuOpenText, weOpenText, thOpenText, frOpenText, saOpenText;
        public Text suCloseText, moCloseText, tuCloseText, weCloseText, thCloseText, frCloseText, saCloseText;
        public PrefabList prefabs;
        public Week newWeek;//Week that is being generated and eventually passed to the main system

        public DailyStaffing() { }

        public void Start() { }

        public void ValidateSubmission()
        {
            bool validForm = true;



            if (validForm)
                SubmitWeek();
        }

        private void SubmitWeek()
        {
            CalculateHourlyNeed();
        }

        private void CalculateHourlyNeed()
        {
            if (sunday)
            {

            }
            if (monday)
            {

            }
            if (tuesday)
            {

            }
            if (wednesday)
            {

            }
            if (thursday)
            {

            }
            if (friday)
            {

            }
            if (saturday)
            {

            }
        }

        public void WeeklyConfigPopup()
        {
            //Weekly Config Popup
            GameObject weeklyConfig = (GameObject)Instantiate(prefabs.prefabList[0], new Vector3(0, 0, 0), Quaternion.identity);
            weeklyConfig.transform.SetParent(this.gameObject.transform);
            weeklyConfig.transform.localScale = new Vector3(1, 1, 1);//Moving transforms seems to screw with the scale, so its set in code for redundancy
            weeklyConfig.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
            //End
        }

        public void SetWeeklyConfig(Week week)
        {
            newWeek = week;
            if (week.sunday)
            {
                suOpenText.text = week.suStartHour.ToString();
                suCloseText.text = week.suEndHour.ToString();
            }
            else
                sunday.SetActive(false);
            if (week.monday)
            {
                moOpenText.text = week.mStartHour.ToString();
                moCloseText.text = week.mEndHour.ToString();
            }
            else
                monday.SetActive(false);
            if (week.tuesday)
            {
                tuOpenText.text = week.tuStartHour.ToString();
                tuCloseText.text = week.tuEndHour.ToString();
            }
            else
                tuesday.SetActive(false);
            if (week.wednesday)
            {
                weOpenText.text = week.wStartHour.ToString();
                weCloseText.text = week.wEndHour.ToString();
            }
            else
                wednesday.SetActive(false);
            if (week.thursday)
            {
                thOpenText.text = week.thStartHour.ToString();
                thCloseText.text = week.thEndHour.ToString();
            }
            else
                thursday.SetActive(false);
            if (week.friday)
            {
                frOpenText.text = week.fStartHour.ToString();
                frCloseText.text = week.fEndHour.ToString();
            }
            else
                friday.SetActive(false);
            if (week.saturday)
            {
                saOpenText.text = week.saStartHour.ToString();
                saCloseText.text = week.saEndHour.ToString();
            }
            else
                saturday.SetActive(false);
        }
    }
}