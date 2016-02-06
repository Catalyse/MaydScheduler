using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Windows;

namespace CoreSys
{
    /// <summary>
    /// Daily Staffing requirements window script.
    /// </summary>
    public class DailyStaffing : Window
    {
        public ScheduleWindow parent;
        public InputField sundayOpen, mondayOpen, tuesdayOpen, wednesdayOpen, thursdayOpen, fridayOpen, saturdayOpen;
        public InputField sundayClose, mondayClose, tuesdayClose, wednesdayClose, thursdayClose, fridayClose, saturdayClose;
        public GameObject sunday, monday, tuesday, wednesday, thursday, friday, saturday;
        public Text suOpenText, moOpenText, tuOpenText, weOpenText, thOpenText, frOpenText, saOpenText;
        public Text suCloseText, moCloseText, tuCloseText, weCloseText, thCloseText, frCloseText, saCloseText;
        public Text suText, mText, tuText, wText, thText, fText, saText;
        public PrefabList prefabs;
        public Week newWeek;//Week that is being generated and eventually passed to the main system
        private int defaultShift = 8;//TODO put this in settings window

        public DailyStaffing() { }

        public void Start() { }

        public void Update()
        {
            ErrorTextFade();
        }

        private void ErrorTextFade()
        {
            if (suText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(suText.color.a, 0, 2 * Time.deltaTime);
                suText.color = new Color(suText.color.r, suText.color.g, suText.color.b, newAlpha);
            }
            else
                suText.enabled = false;
            if (mText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(mText.color.a, 0, 2 * Time.deltaTime);
                mText.color = new Color(mText.color.r, mText.color.g, mText.color.b, newAlpha);
            }
            else
                mText.enabled = false;
            if (tuText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(tuText.color.a, 0, 2 * Time.deltaTime);
                tuText.color = new Color(tuText.color.r, tuText.color.g, tuText.color.b, newAlpha);
            }
            else
                tuText.enabled = false;
            if (wText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(wText.color.a, 0, 2 * Time.deltaTime);
                wText.color = new Color(wText.color.r, wText.color.g, wText.color.b, newAlpha);
            }
            else
                wText.enabled = false;
            if (thText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(thText.color.a, 0, 2 * Time.deltaTime);
                thText.color = new Color(thText.color.r, thText.color.g, thText.color.b, newAlpha);
            }
            else
                thText.enabled = false;
            if (fText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(fText.color.a, 0, 2 * Time.deltaTime);
                fText.color = new Color(fText.color.r, fText.color.g, fText.color.b, newAlpha);
            }
            else
                fText.enabled = false;
            if (saText.color.a > .1)
            {
                float newAlpha = Mathf.Lerp(saText.color.a, 0, 2 * Time.deltaTime);
                saText.color = new Color(saText.color.r, saText.color.g, saText.color.b, newAlpha);
            }
            else
                saText.enabled = false;
        }

        public void ValidateSubmission()
        {
            bool validForm = true;
            if (sundayOpen.text == "" || sundayClose.text == "")
            {
                suText.enabled = true;
                suText.color = new Color(suText.color.r, suText.color.g, suText.color.b, 50);
                validForm = false;
            }
            if (mondayOpen.text == "" || mondayClose.text == "")
            {
                mText.enabled = true;
                mText.color = new Color(mText.color.r, mText.color.g, mText.color.b, 50);
                validForm = false;
            }
            if (tuesdayOpen.text == "" || tuesdayClose.text == "")
            {
                tuText.enabled = true;
                tuText.color = new Color(tuText.color.r, tuText.color.g, tuText.color.b, 50);
                validForm = false;
            }
            if (wednesdayOpen.text == "" || wednesdayClose.text == "")
            {
                wText.enabled = true;
                wText.color = new Color(wText.color.r, wText.color.g, wText.color.b, 50);
                validForm = false;
            }
            if (thursdayOpen.text == "" || thursdayClose.text == "")
            {
                thText.enabled = true;
                thText.color = new Color(thText.color.r, thText.color.g, thText.color.b, 50);
                validForm = false;
            }
            if (fridayOpen.text == "" || fridayClose.text == "")
            {
                fText.enabled = true;
                fText.color = new Color(fText.color.r, fText.color.g, fText.color.b, 50);
                validForm = false;
            }
            if (saturdayOpen.text == "" || saturdayClose.text == "")
            {
                saText.enabled = true;
                saText.color = new Color(saText.color.r, saText.color.g, saText.color.b, 50);
                validForm = false;
            }


            if (validForm)
                SubmitWeek();
        }

        private void SubmitWeek()
        {
            //CalculateHourlyNeed();
        }

        public void WeeklyConfigPopup()
        {
            //Weekly Config Popup
            WindowInstantiator.SpawnWindow(prefabs.prefabList[0], this.gameObject);
            //End
        }

        public void SetWeeklyConfig(Week week)
        {
            newWeek = week;
            
            if (week.sunday.activeDay)
            {
                suOpenText.text = week.sunday.openTime.ToString();
                suCloseText.text = week.sunday.closeTime.ToString();
            }
            else
                sunday.SetActive(false);
            if (week.monday.activeDay)
            {
                moOpenText.text = week.monday.openTime.ToString();
                moCloseText.text = week.monday.closeTime.ToString();
            }
            else
                monday.SetActive(false);
            if (week.tuesday.activeDay)
            {
                tuOpenText.text = week.tuesday.openTime.ToString();
                tuCloseText.text = week.tuesday.closeTime.ToString();
            }
            else
                tuesday.SetActive(false);
            if (week.wednesday.activeDay)
            {
                weOpenText.text = week.wednesday.openTime.ToString();
                weCloseText.text = week.wednesday.closeTime.ToString();
            }
            else
                wednesday.SetActive(false);
            if (week.thursday.activeDay)
            {
                thOpenText.text = week.thursday.openTime.ToString();
                thCloseText.text = week.thursday.closeTime.ToString();
            }
            else
                thursday.SetActive(false);
            if (week.friday.activeDay)
            {
                frOpenText.text = week.friday.openTime.ToString();
                frCloseText.text = week.friday.closeTime.ToString();
            }
            else
                friday.SetActive(false);
            if (week.saturday.activeDay)
            {
                saOpenText.text = week.saturday.openTime.ToString();
                saCloseText.text = week.saturday.closeTime.ToString();
            }
            else
                saturday.SetActive(false);
        }
    }
}