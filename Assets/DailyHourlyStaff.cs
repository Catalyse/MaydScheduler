using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public class DailyHourlyStaff : MonoBehaviour
    {
        public List<InputField> sundayList, mondayList, tuesdayList, wednesdayList, thursdayList, fridayList, saturdayList;
        public Week newWeek;
        private int startXVal = 100;
        private int xValIteration = 26;

        public DailyHourlyStaff()
        {
            WeeklyConfig config = new WeeklyConfig();
        }
        // Use this for initialization
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }

        public void SetAllClick(int button)
        {
            if (button == 0)//sunday
            {

            }
            else if (button == 1)//monday
            {

            }
            else if (button == 2)//tuesday
            {

            }
            else if (button == 3)//wednesday
            {

            }
            else if (button == 4)//thursday
            {

            }
            else if (button == 5)//friday
            {

            }
            else if (button == 6)//saturday
            {

            }
        }

        public void SetWeeklyConfig(Week setWeek)
        {
            newWeek = setWeek;
        }
    }
}