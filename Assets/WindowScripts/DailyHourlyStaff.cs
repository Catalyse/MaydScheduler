using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Types;

namespace CoreSys
{
    /// <summary>
    /// Daily Hourly Staffing requirements window script.
    /// </summary>
    public class DailyHourlyStaff : Window
    {
        private bool configured = false;//This stays false till weekly has been run
        public GameObject configuredModeParent, needConfigModeParent;
        public PrefabList prefabs;
        public List<InputField> sundayList, mondayList, tuesdayList, wednesdayList, thursdayList, fridayList, saturdayList;
        private List<HourColumn> hourList = new List<HourColumn>();
        public InputField sundayInput, mondayInput, tuesdayInput, wednesdayInput, thursdayInput, fridayInput, saturdayInput;
        public Week newWeek;//Week that is being generated and eventually passed to the main system
        private int startXVal = 100;//Starting point for hour placement.
        private int xValIteration = 26;//This is the step for x val placement in the window when generating hours. If the window size is changed this needs to be adjusted as well.

        public DailyHourlyStaff() { }

        public void Start()
        {
            
        }

        public void SetAllClick(int button)
        {
            if (button == 0)//sunday
            {
                for (int i = 0; i < sundayList.Count; i++)
                {
                    sundayList[i].text = sundayInput.text;
                }
            }
            else if (button == 1)//monday
            {
                for (int i = 0; i < mondayList.Count; i++)
                {
                    mondayList[i].text = mondayInput.text;
                }
            }
            else if (button == 2)//tuesday
            {
                for (int i = 0; i < tuesdayList.Count; i++)
                {
                    tuesdayList[i].text = tuesdayInput.text;
                }
            }
            else if (button == 3)//wednesday
            {
                for (int i = 0; i < wednesdayList.Count; i++)
                {
                    wednesdayList[i].text = wednesdayInput.text;
                }
            }
            else if (button == 4)//thursday
            {
                for (int i = 0; i < thursdayList.Count; i++)
                {
                    thursdayList[i].text = thursdayInput.text;
                }
            }
            else if (button == 5)//friday
            {
                for (int i = 0; i < fridayList.Count; i++)
                {
                    fridayList[i].text = fridayInput.text;
                }
            }
            else if (button == 6)//saturday
            {
                for (int i = 0; i < saturdayList.Count; i++)
                {
                    saturdayList[i].text = saturdayInput.text;
                }
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

        public void SetWeeklyConfig(Week setWeek)
        {
            newWeek = setWeek;
            GenerateHourColumns();
        }

        private void GenerateHourColumns()
        {
            int k = 0;
            for (int i = newWeek.earliestStart; i <= newWeek.latestEnd; i++)
            {
                GameObject newHour = (GameObject)Instantiate(prefabs.prefabList[1], new Vector3(0, 0, 0), Quaternion.identity);
                newHour.transform.parent = transform;
                newHour.transform.localScale = new Vector3(1, 1, 1);
                newHour.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((startXVal+(xValIteration*k)), -145, 0);
                k++;
                HourColumn newColumn = newHour.GetComponent<HourColumn>();
                newColumn.SetHourColumn(i,(newWeek.suStartHour<=i && newWeek.suEndHour >= i),(newWeek.mStartHour<=i && newWeek.mEndHour >= i),
                    (newWeek.tuStartHour<=i && newWeek.tuEndHour >= i),(newWeek.wStartHour<=i && newWeek.wEndHour >= i),(newWeek.thStartHour<=i && newWeek.thEndHour >= i),
                    (newWeek.fStartHour<=i && newWeek.fEndHour >= i),(newWeek.saStartHour<=i && newWeek.saEndHour >= i));
            }
        }
    }
}