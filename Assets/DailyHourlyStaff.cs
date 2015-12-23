using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public class DailyHourlyStaff : MonoBehaviour
    {
        public PrefabList prefabs;
        public List<InputField> sundayList, mondayList, tuesdayList, wednesdayList, thursdayList, fridayList, saturdayList;
        private List<HourColumn> hourList = new List<HourColumn>();
        public InputField sundayInput, mondayInput, tuesdayInput, wednesdayInput, thursdayInput, fridayInput, saturdayInput;
        public Week newWeek;
        private int startXVal = 100;
        private int xValIteration = 26;

        public DailyHourlyStaff()
        {
            WeeklyConfig config = new WeeklyConfig();
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

        public void SetWeeklyConfig(Week setWeek)
        {
            newWeek = setWeek;
            GenerateHourColumns();
            //int k = 0;
            //for (int i = 0; i < hourList.Count; i++)
            //{
            //    hourList[i].transform.position = new Vector3((startXVal + (xValIteration * k)), -145, 0);
            //    k++;
            //}
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