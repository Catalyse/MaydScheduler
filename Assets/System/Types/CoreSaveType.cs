using UnityEngine;
using System;
using System.Collections.Generic;

namespace CoreSys
{
    public class CoreSaveType
    {
        public SerializableDictionary<DateTime, Week> weekList = new SerializableDictionary<DateTime, Week>();
        public string GenerationDate;
        public string LastModified;

        public CoreSaveType() { }
    }
}