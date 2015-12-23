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
        public Text sText, mText, tuText, wText, thText, fText, saText;
        public InputField suStart, mStart, tuStart, wStart, thStart, fStart, saStart;
        public InputField suEnd, mEnd, tuEnd, wEnd, thEnd, fEnd, saEnd;

        public void Update()
        {
            if (sText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(sText.color.a, 0, Time.deltaTime);
                sText.color = new Color(sText.color.r, sText.color.g, sText.color.b, newAlpha);
            }
            if (sText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(mText.color.a, 0, Time.deltaTime);
                mText.color = new Color(mText.color.r, mText.color.g, mText.color.b, newAlpha);
            }
            if (sText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(tuText.color.a, 0, Time.deltaTime);
                tuText.color = new Color(tuText.color.r, tuText.color.g, tuText.color.b, newAlpha);
            }
            if (sText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(wText.color.a, 0, Time.deltaTime);
                wText.color = new Color(wText.color.r, wText.color.g, wText.color.b, newAlpha);
            }
            if (sText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(thText.color.a, 0, Time.deltaTime);
                thText.color = new Color(thText.color.r, thText.color.g, thText.color.b, newAlpha);
            }
            if (sText.color.a != 0)
            {
                float newAlpha = Mathf.Lerp(fText.color.a, 0, Time.deltaTime);
                fText.color = new Color(fText.color.r, fText.color.g, fText.color.b, newAlpha);
            }
            if (sText.color.a != 0)
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
                    sText.color = new Color(sText.color.r, sText.color.g, sText.color.b, 255);
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
                
            }
            else
            {
                suStart.text = "0";
                suEnd.text = "0";
            }
            if (tuesday.isOn)
            {
                
            }
            else
            {
                suStart.text = "0";
                suEnd.text = "0";
            }
            if (wednesday.isOn)
            {
                
            }
            else
            {
                suStart.text = "0";
                suEnd.text = "0";
            }
            if (thursday.isOn)
            {
                
            }
            else
            {
                suStart.text = "0";
                suEnd.text = "0";
            }
            if (friday.isOn)
            {
                
            }
            else
            {
                suStart.text = "0";
                suEnd.text = "0";
            }
            if (saturday.isOn)
            {
                
            }
            else
            {
                saStart.text = "0";
                saEnd.text = "0";
            }
        }
    }
}