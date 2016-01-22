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
        public InputField sundayOpen, mondayOpen, tuesdayOpen, wednesdayOpen, thursdayOpen, fridayOpen, saturdayOpen;
        public InputField sundayClose, mondayClose, tuesdayClose, wednesdayClose, thursdayClose, fridayClose, saturdayClose;
        public GameObject sunday, monday, tuesday, wednesday, thursday, friday, saturday;
        public Text suOpenText, moOpenText, tuOpenText, weOpenText, thOpenText, frOpenText, saOpenText;
        public Text suCloseText, moCloseText, tuCloseText, weCloseText, thCloseText, frCloseText, saCloseText;
        public PrefabList prefabs;
        public Week newWeek;//Week that is being generated and eventually passed to the main system
        private int defaultShift = 8;//TODO put this in settings window

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
            //CalculateHourlyNeed();
        }

        /*
        private void CalculateHourlyNeed()
        {
            if (newWeek.sunday)
            {
                SetHourlyNeedCounts(newWeek.suStartHour, newWeek.suEndHour, newWeek.sSundayHourReqs, int.Parse(sundayOpen.text), int.Parse(sundayClose.text));
            }
            if (newWeek.monday)
            {
                SetHourlyNeedCounts(newWeek.mStartHour, newWeek.mEndHour, newWeek.sMondayHourReqs, int.Parse(mondayOpen.text), int.Parse(mondayClose.text));
            }
            if (newWeek.tuesday)
            {
                SetHourlyNeedCounts(newWeek.tuStartHour, newWeek.tuEndHour, newWeek.sTuesdayHourReqs, int.Parse(tuesdayOpen.text), int.Parse(tuesdayClose.text));
            }
            if (newWeek.wednesday)
            {
                SetHourlyNeedCounts(newWeek.wStartHour, newWeek.wEndHour, newWeek.sWednesdayHourReqs, int.Parse(wednesdayOpen.text), int.Parse(wednesdayClose.text));
            }
            if (newWeek.thursday)
            {
                SetHourlyNeedCounts(newWeek.thStartHour, newWeek.thEndHour, newWeek.sThursdayHourReqs, int.Parse(thursdayOpen.text), int.Parse(thursdayClose.text));
            }
            if (newWeek.friday)
            {
                SetHourlyNeedCounts(newWeek.fStartHour, newWeek.fEndHour, newWeek.sFridayHourReqs, int.Parse(fridayOpen.text), int.Parse(fridayClose.text));
            }
            if (newWeek.saturday)
            {
                SetHourlyNeedCounts(newWeek.saStartHour, newWeek.saEndHour, newWeek.sSaturdayHourReqs, int.Parse(saturdayOpen.text), int.Parse(saturdayClose.text));
            }
        }*/

        private void SetHourlyNeedCounts(int startTime, int endTime, Dictionary<int,int> dictionary, int hourlyNeedOpen, int hourlyNeedClose)
        {
            for (int i = startTime; i <= (startTime + defaultShift); i++)
            {
                dictionary[i] += hourlyNeedOpen;
            }
            for (int i = endTime; i > (endTime - defaultShift); i--)
            {
                dictionary[i] += hourlyNeedClose;
            }
        }

        /*
        private void InitializeWeeklyDictionaries()
        {
            if (newWeek.sunday)
            {
                for (int i = newWeek.suStartHour; i <= newWeek.suEndHour; i++)
                {
                    newWeek.sSundayHourReqs.Add(i, 0);
                    newWeek.eSundayHourReqs.Add(i, 0);
                }
            }
            if (newWeek.monday)
            {
                for (int i = newWeek.mStartHour; i <= newWeek.mEndHour; i++)
                {
                    newWeek.sMondayHourReqs.Add(i, 0);
                    newWeek.eMondayHourReqs.Add(i, 0);
                }
            }
            if (newWeek.tuesday)
            {
                for (int i = newWeek.tuStartHour; i <= newWeek.tuEndHour; i++)
                {
                    newWeek.sTuesdayHourReqs.Add(i, 0);
                    newWeek.eTuesdayHourReqs.Add(i, 0);
                }
            }
            if (newWeek.wednesday)
            {
                for (int i = newWeek.wStartHour; i <= newWeek.wEndHour; i++)
                {
                    newWeek.sWednesdayHourReqs.Add(i, 0);
                    newWeek.eWednesdayHourReqs.Add(i, 0);
                }
            }
            if (newWeek.thursday)
            {
                for (int i = newWeek.thStartHour; i <= newWeek.thEndHour; i++)
                {
                    newWeek.sThursdayHourReqs.Add(i, 0);
                    newWeek.eThursdayHourReqs.Add(i, 0);
                }
            }
            if (newWeek.friday)
            {
                for (int i = newWeek.fStartHour; i <= newWeek.fEndHour; i++)
                {
                    newWeek.sFridayHourReqs.Add(i, 0);
                    newWeek.eFridayHourReqs.Add(i, 0);
                }
            }
            if (newWeek.saturday)
            {
                for (int i = newWeek.saStartHour; i <= newWeek.saEndHour; i++)
                {
                    newWeek.sSaturdayHourReqs.Add(i, 0);
                    newWeek.eSaturdayHourReqs.Add(i, 0);
                }
            }
        }*/

        public void WeeklyConfigPopup()
        {
            //Weekly Config Popup
            WindowInstantiator.SpawnWindow(prefabs.prefabList[0], this.gameObject);
            //End
        }

        public void SetWeeklyConfig(Week week)
        {
            newWeek = week;
            //InitializeWeeklyDictionaries();
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