using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Windows;

namespace CoreSys
{
    public class ChooseWeek : Window
    {
        public ScheduleWindow parent;
        private int weekDistance = 4;//This is how many weeks forward it will check/generate
        public DateTime today;
        public List<Week> weekList = new List<Week>();

        /// <summary>
        /// This method will using the current date and find all weeks moving forward within a limited timeframe.
        /// </summary>
        public void FindWeeks()
        {
            today = DateTime.Now;

        }
    }
}