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
        public InputField suStart, mStart, tuStart, wStart, thStart, fStart, saStart;
        public InputField suEnd, mEnd, tuEnd, wEnd, thEnd, fEnd, saEnd;

        public void SubmitPushed()
        {
            Week newWeek = new Week(int.Parse(suStart.text), int.Parse(mStart.text), int.Parse(tuStart.text), int.Parse(wStart.text), int.Parse(thStart.text), 
                int.Parse(fStart.text), int.Parse(saStart.text), int.Parse(suEnd.text), int.Parse(mEnd.text), int.Parse(tuEnd.text), int.Parse(wEnd.text), 
                int.Parse(thEnd.text), int.Parse(fEnd.text), int.Parse(saEnd.text), sunday.isOn, monday.isOn, tuesday.isOn, wednesday.isOn, thursday.isOn, friday.isOn, saturday.isOn);
        }
    }
}