using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Windows
{
    public class AvailabilityWindow : Window
    {
        public ScheduleWindow parent;
        public Text title;
        public Toggle sun, mon, tue, wed, thu, fri, sat;
        private Availability temp;

        public void SetToggles(Availability avail, string empName)
        {
            title.text = "Availability for " + empName;
            temp = avail;
            if (avail.sunday.available)
                sun.isOn = true;
            if (avail.monday.available)
                mon.isOn = true;
            if (avail.tuesday.available)
                tue.isOn = true;
            if (avail.wednesday.available)
                wed.isOn = true;
            if (avail.thursday.available)
                thu.isOn = true;
            if (avail.friday.available)
                fri.isOn = true;
            if (avail.saturday.available)
                sat.isOn = true;
        }

        public void SubmitChanges()
        {
            temp.sunday.available = sun.isOn;
            temp.monday.available = mon.isOn;
            temp.tuesday.available = tue.isOn;
            temp.wednesday.available = wed.isOn;
            temp.thursday.available = thu.isOn;
            temp.friday.available = fri.isOn;
            temp.saturday.available = sat.isOn;
            parent.AvailLoopCallback(temp);
        }

        public void CancelChanges()
        {
            parent.AvailLoopCallback(temp);
        }
    }
}