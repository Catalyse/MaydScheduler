using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Windows;

/// <summary>
/// TODO Set a system for implementing defaults
/// </summary>
namespace CoreSys
{
    public class WeeklyConfig : Window
    {
        public ScheduleWindow parent;
        public Toggle sunday, monday, tuesday, wednesday, thursday, friday, saturday;
        public Text suText, mText, tuText, wText, thText, fText, saText;
        public InputField suStart, mStart, tuStart, wStart, thStart, fStart, saStart;
        public InputField suEnd, mEnd, tuEnd, wEnd, thEnd, fEnd, saEnd;

        public WeeklyConfig() { }

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

        private void SubmitPushed()
        {
            parent.currentWeek.SetWeek(int.Parse(suStart.text), int.Parse(mStart.text), int.Parse(tuStart.text), int.Parse(wStart.text), int.Parse(thStart.text), 
                int.Parse(fStart.text), int.Parse(saStart.text), int.Parse(suEnd.text), int.Parse(mEnd.text), int.Parse(tuEnd.text), int.Parse(wEnd.text), 
                int.Parse(thEnd.text), int.Parse(fEnd.text), int.Parse(saEnd.text), sunday.isOn, monday.isOn, tuesday.isOn, wednesday.isOn, thursday.isOn, friday.isOn, saturday.isOn);
            parent.DailyStaffing();//This needs to be changed IF you use the hourly staffing XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX///
            transform.gameObject.SetActive(false);//Disable window when done
        }

        private void InputValidation() //This method will correct times put in incorrectly
        {
            //todo
        }

        public void CheckInputBoxes()
        {
            bool validSubmit = true;//Stays true unless value fails to be met

            if (sunday.isOn)
            {
                if (suStart.text == "" || suEnd.text == "")
                {
                    suText.enabled = true;
                    suText.color = new Color(suText.color.r, suText.color.g, suText.color.b, 50);
                    validSubmit = false;
                }
            }
            else
            {
                suStart.text = "0";
                suEnd.text = "0";
            }
            if (monday.isOn)
            {
                if (mStart.text == "" || mEnd.text == "")
                {
                    mText.enabled = true;
                    mText.color = new Color(mText.color.r, mText.color.g, mText.color.b, 50);
                    validSubmit = false;
                }
            }
            else
            {
                mStart.text = "0";
                mEnd.text = "0";
            }
            if (tuesday.isOn)
            {
                if (tuStart.text == "" || tuEnd.text == "")
                {
                    tuText.enabled = true;
                    tuText.color = new Color(tuText.color.r, tuText.color.g, tuText.color.b, 50);
                    validSubmit = false;
                }
            }
            else
            {
                tuStart.text = "0";
                tuEnd.text = "0";
            }
            if (wednesday.isOn)
            {
                if (wStart.text == "" || wEnd.text == "")
                {
                    wText.enabled = true;
                    wText.color = new Color(wText.color.r, wText.color.g, wText.color.b, 50);
                    validSubmit = false;
                }
            }
            else
            {
                wStart.text = "0";
                wEnd.text = "0";
            }
            if (thursday.isOn)
            {
                if (thStart.text == "" || thEnd.text == "")
                {
                    thText.enabled = true;
                    thText.color = new Color(thText.color.r, thText.color.g, thText.color.b, 50);
                    validSubmit = false;
                }
            }
            else
            {
                thStart.text = "0";
                thEnd.text = "0";
            }
            if (friday.isOn)
            {
                if (fStart.text == "" || fEnd.text == "")
                {
                    fText.enabled = true;
                    fText.color = new Color(fText.color.r, fText.color.g, fText.color.b, 50);
                    validSubmit = false;
                }
            }
            else
            {
                fStart.text = "0";
                fEnd.text = "0";
            }
            if (saturday.isOn)
            {
                if (saStart.text == "" || saEnd.text == "")
                {
                    saText.enabled = true;
                    saText.color = new Color(saText.color.r, saText.color.g, saText.color.b, 50);
                    validSubmit = false;
                }
            }
            else
            {
                saStart.text = "0";
                saEnd.text = "0";
            }
            if (validSubmit)
            {
                SubmitPushed();
            }
        }
    }
}