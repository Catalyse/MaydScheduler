using UnityEngine;
using System;
using System.Collections.Generic;

namespace CoreSys
{
    public class CoreSaveType
    {
        public int defaultShift, minShift, maxShift;//Need to make config window
        public int defaultOpenAvail, defaultCloseAvail;
        public float skillLevelCap;
        public SerializableDictionary<int, string> positionList = new SerializableDictionary<int, string>();
        public SerializableDictionary<DateTime, Week> weekList = new SerializableDictionary<DateTime, Week>();
        public List<string> savedFileList = new List<string>();
        public string GenerationDate;

        public CoreSaveType() { }
    }
}