using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys.Types
{
    public class Week
    {
        public int earliestStart, latestEnd;
        public int suStartHour, mStartHour, tuStartHour, wStartHour, thStartHour, fStartHour, saStartHour;
        public int suEndHour, mEndHour, tuEndHour, wEndHour, thEndHour, fEndHour, saEndHour;
        public int suHoursOpen, mHoursOpen, tuHoursOpen, wHoursOpen, thHoursOpen, fHoursOpen, saHoursOpen;
        public bool sunday, monday, tuesday, wednesday, thursday, friday, saturday;
        public Dictionary<int, int> sSundayHourReqs, sMondayHourReqs, sTuesdayHourReqs, sWednesdayHourReqs, sThursdayHourReqs, sFridayHourReqs, sSaturdayHourReqs;//Solutions Spec Hour Reqs
        public Dictionary<int, int> eSundayHourReqs, eMondayHourReqs, eTuesdayHourReqs, eWednesdayHourReqs, eThursdayHourReqs, eFridayHourReqs, eSaturdayHourReqs;//Experience Spec Hour Reqs
        // Use this for initialization

        public Week() { }

        public Week(int _suStartHour, int _mStartHour, int _tuStartHour, int _wStartHour, int _thStartHour, int _fStartHour, int _saStartHour, int _suEndHour, int _mEndHour,
            int _tuEndHour, int _wEndHour, int _thEndHour, int _fEndHour, int _saEndHour, bool sun, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat)
        {
            suStartHour = _suStartHour;
            mStartHour = _mStartHour;
            tuStartHour = _tuStartHour;
            wStartHour = _wStartHour;
            thStartHour = _thStartHour;
            fStartHour = _fStartHour;
            saStartHour = _saStartHour;
            suEndHour = _suEndHour;
            mEndHour = _mEndHour;
            tuEndHour = _tuEndHour;
            wEndHour = _wEndHour;
            thEndHour = _thEndHour;
            fEndHour = _fEndHour;
            saEndHour = _saEndHour;
            //Begin bool set
            sunday = sun;
            monday = mon;
            tuesday = tue;
            wednesday = wed;
            thursday = thu;
            friday = fri;
            saturday = sat;
            //End bool set
            HoursOpenCalc();
        }

        private void HoursOpenCalc()
        {
            if (sunday)
            {
                earliestStart = suStartHour;
                latestEnd = suEndHour;
                suHoursOpen = suEndHour - suStartHour;
            }
            if (monday)
            {
                if (earliestStart > mStartHour)
                    earliestStart = mStartHour;
                if (latestEnd < mEndHour)
                    latestEnd = mEndHour;
                mHoursOpen = mEndHour - mStartHour;
            }
            if (tuesday)
            {
                if (earliestStart > tuStartHour)
                    earliestStart = tuStartHour;
                if (latestEnd < tuEndHour)
                    latestEnd = tuEndHour;
                tuHoursOpen = tuEndHour - tuStartHour;
            }
            if (wednesday)
            {
                if (earliestStart > wStartHour)
                    earliestStart = wStartHour;
                if (latestEnd < wEndHour)
                    latestEnd = wEndHour;
                wHoursOpen = wEndHour - wStartHour;
            }
            if (thursday)
            {
                if (earliestStart > thStartHour)
                    earliestStart = thStartHour;
                if (latestEnd < thEndHour)
                    latestEnd = thEndHour;
                thHoursOpen = thEndHour - thStartHour;
            }
            if (friday)
            {
                if (earliestStart > fStartHour)
                    earliestStart = fStartHour;
                if (latestEnd < fEndHour)
                    latestEnd = fEndHour;
                fHoursOpen = fEndHour - fStartHour;
            }
            if (saturday)
            {
                if (earliestStart > saStartHour)
                    earliestStart = saStartHour;
                if (latestEnd < saEndHour)
                    latestEnd = saEndHour;
                saHoursOpen = saEndHour - saStartHour;
            }
        }
    }
}