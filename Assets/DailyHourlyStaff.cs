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
            //WeeklyConfigWindow();
        }
        // Use this for initialization
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }

        //private Week WeeklyConfigWindow()
        //{
            
        //}
    }
}