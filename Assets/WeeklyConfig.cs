using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// TODO Set a system for implementing defaults
/// </summary>
namespace CoreSys
{
    public class WeeklyConfig : MonoBehaviour
    {
        public Toggle sunday, monday, tuesday, wednesday, thursday, friday, saturday;
        public Text suText, mText, tuText, wText, thText, fText, saText;
        public InputField suStart, mStart, tuStart, wStart, thStart, fStart, saStart;
        public InputField suEnd, mEnd, tuEnd, wEnd, thEnd, fEnd, saEnd;

        public void Update()
        {
            if (suText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(suText.color.a, 0, Time.deltaTime);
                suText.color = new Color(suText.color.r, suText.color.g, suText.color.b, newAlpha);
            }
            if (mText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(mText.color.a, 0, Time.deltaTime);
                mText.color = new Color(mText.color.r, mText.color.g, mText.color.b, newAlpha);
            }
            if (tuText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(tuText.color.a, 0, Time.deltaTime);
                tuText.color = new Color(tuText.color.r, tuText.color.g, tuText.color.b, newAlpha);
            }
            if (wText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(wText.color.a, 0, Time.deltaTime);
                wText.color = new Color(wText.color.r, wText.color.g, wText.color.b, newAlpha);
            }
            if (thText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(thText.color.a, 0, Time.deltaTime);
                thText.color = new Color(thText.color.r, thText.color.g, thText.color.b, newAlpha);
            }
            if (fText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(fText.color.a, 0, Time.deltaTime);
                fText.color = new Color(fText.color.r, fText.color.g, fText.color.b, newAlpha);
            }
            if (saText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(saText.color.a, 0, Time.deltaTime);
                saText.color = new Color(saText.color.r, saText.color.g, saText.color.b, newAlpha);
            }
        }

        public void SubmitPushed()
        {
            Week newWeek = new Week(int.Parse(suStart.text), int.Parse(mStart.text), int.Parse(tuStart.text), int.Parse(wStart.text), int.Parse(thStart.text), 
                int.Parse(fStart.text), int.Parse(saStart.text), int.Parse(suEnd.text), int.Parse(mEnd.text), int.Parse(tuEnd.text), int.Parse(wEnd.text), 
                int.Parse(thEnd.text), int.Parse(fEnd.text), int.Parse(saEnd.text), sunday.isOn, monday.isOn, tuesday.isOn, wednesday.isOn, thursday.isOn, friday.isOn, saturday.isOn);
        }

        private void CheckInputBoxes()
        {
            bool validSubmit = true;//Stays true unless value fails to be met

            if (sunday.isOn)
            {
                if (suStart.text == "" || suEnd.text == "")
                {
                    suText.color = new Color(suText.color.r, suText.color.g, suText.color.b, 255);
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
                    mText.color = new Color(mText.color.r, mText.color.g, mText.color.b, 255);
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
                    tuText.color = new Color(tuText.color.r, tuText.color.g, tuText.color.b, 255);
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
                    wText.color = new Color(wText.color.r, wText.color.g, wText.color.b, 255);
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
                    thText.color = new Color(thText.color.r, thText.color.g, thText.color.b, 255);
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
                    fText.color = new Color(fText.color.r, fText.color.g, fText.color.b, 255);
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
                    saText.color = new Color(saText.color.r, saText.color.g, saText.color.b, 255);
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