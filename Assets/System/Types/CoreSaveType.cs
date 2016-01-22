using UnityEngine;
using System.Collections.Generic;

namespace CoreSys
{
    public class CoreSaveType
    {
        public bool systemInitialized;
        public int currentWeekID;
        public int defaultShift;
        public Dictionary<int, string> positionList = new Dictionary<int, string>();
        public Dictionary<int, DailySchedule> dayList = new Dictionary<int, DailySchedule>();
        public Dictionary<int, Week> weekList = new Dictionary<int, Week>();
        public List<string> savedFileList = new List<string>();
        public string GenerationDate;
    }
}