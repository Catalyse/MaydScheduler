using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Windows
{
    public class AvailChangePrompt : MonoBehaviour
    {
        public ScheduleWindow parent;

        public void Change()
        {
            parent.CheckTempDaysOff();
        }

        public void NoChange()
        {
            parent.AvailChangeCancel();
        }
    }
}