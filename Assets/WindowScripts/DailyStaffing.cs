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
        public Text suOpenText, moOpenText, tuOpenText, weOpenText, thOpenText, frOpenText, saOpenText;
        public Text suCloseText, moCloseText, tuCloseText, weCloseText, thCloseText, frCloseText, saCloseText;
        public PrefabList prefabs;
        public Week newWeek;//Week that is being generated and eventually passed to the main system
        private int startXVal = 100;//Starting point for hour placement.
        private int xValIteration = 26;//This is the step for x val placement in the window when generating hours. If the window size is changed this needs to be adjusted as well.

        public DailyStaffing() { }

        public void Start()
        {

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
            suOpenText.text = week.suStartHour.ToString();
            suCloseText.text = week.suEndHour.ToString();
            moOpenText.text = week.mStartHour.ToString();
            moCloseText.text = week.mEndHour.ToString();
            tuOpenText.text = week.tuStartHour.ToString();
            tuCloseText.text = week.tuEndHour.ToString();
            weOpenText.text = week.wStartHour.ToString();
            weCloseText.text = week.wEndHour.ToString();
            thOpenText.text = week.thStartHour.ToString();
            thCloseText.text = week.thEndHour.ToString();
            frOpenText.text = week.fStartHour.ToString();
            frCloseText.text = week.fEndHour.ToString();
            saOpenText.text = week.saStartHour.ToString();
            saCloseText.text = week.saEndHour.ToString();
        }
    }
}