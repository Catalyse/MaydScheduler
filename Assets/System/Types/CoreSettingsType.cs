using UnityEngine;
using System;
using System.Collections.Generic;

namespace CoreSys
{
    public class CoreSettingsType
    {
        public int defaultShift, minShift, maxShift;//Need to make config window
        public int defaultOpenAvail, defaultCloseAvail;
        public float skillLevelCap;
        public SerializableDictionary<int, string> positionList = new SerializableDictionary<int, string>();
        public List<string> savedFileList = new List<string>();
        public string GenerationDate;
        public string LastModified;

        public CoreSettingsType() { }
    }
}