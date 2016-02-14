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
        private int currentSubmit, maxSubmit;

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
            
        }

        public void SetWeeklyConfig()
        {
            currentSubmit = 0;
            maxSubmit = CoreSystem.positionList.Count;

            if (parent.currentWeek.sunday.activeDay)
            {
                suOpenText.text = parent.currentWeek.sunday.openTime.ToString();
                suCloseText.text = parent.currentWeek.sunday.closeTime.ToString();
            }
            else
                sunday.SetActive(false);
            if (parent.currentWeek.monday.activeDay)
            {
                moOpenText.text = parent.currentWeek.monday.openTime.ToString();
                moCloseText.text = parent.currentWeek.monday.closeTime.ToString();
            }
            else
                monday.SetActive(false);
            if (parent.currentWeek.tuesday.activeDay)
            {
                tuOpenText.text = parent.currentWeek.tuesday.openTime.ToString();
                tuCloseText.text = parent.currentWeek.tuesday.closeTime.ToString();
            }
            else
                tuesday.SetActive(false);
            if (parent.currentWeek.wednesday.activeDay)
            {
                weOpenText.text = parent.currentWeek.wednesday.openTime.ToString();
                weCloseText.text = parent.currentWeek.wednesday.closeTime.ToString();
            }
            else
                wednesday.SetActive(false);
            if (parent.currentWeek.thursday.activeDay)
            {
                thOpenText.text = parent.currentWeek.thursday.openTime.ToString();
                thCloseText.text = parent.currentWeek.thursday.closeTime.ToString();
            }
            else
                thursday.SetActive(false);
            if (parent.currentWeek.friday.activeDay)
            {
                frOpenText.text = parent.currentWeek.friday.openTime.ToString();
                frCloseText.text = parent.currentWeek.friday.closeTime.ToString();
            }
            else
                friday.SetActive(false);
            if (parent.currentWeek.saturday.activeDay)
            {
                saOpenText.text = parent.currentWeek.saturday.openTime.ToString();
                saCloseText.text = parent.currentWeek.saturday.closeTime.ToString();
            }
            else
                saturday.SetActive(false);
        }
    }
}