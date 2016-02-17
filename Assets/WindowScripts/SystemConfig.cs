using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CoreSys;

namespace CoreSys.Windows
{
    public class SystemConfig : Window
    {
        public GameObject addEmpPos;
        public InputField shiftLength, minShift, maxShift, maxSkill, defaultOpen, defaultClose;
        public Dictionary<int, string> positionList = new Dictionary<int, string>();

        public void Start()
        {
            if (CoreSystem.coreSaveLoaded)
            {
                shiftLength.text = CoreSystem.defaultShift.ToString();
                minShift.text = CoreSystem.minShift.ToString();
                maxShift.text = CoreSystem.maxShift.ToString();
                maxSkill.text = CoreSystem.skillLevelCap.ToString();
                defaultOpen.text = CoreSystem.defaultOpenAvail.ToString();
                defaultClose.text = CoreSystem.defaultCloseAvail.ToString();
                positionList = CoreSystem.positionList;
            }
        }

        public void SubmitSettings()
        {
            CoreSystem.defaultShift = int.Parse(shiftLength.text);
            CoreSystem.minShift = int.Parse(minShift.text);
            CoreSystem.maxShift = int.Parse(maxShift.text);
            CoreSystem.skillLevelCap = int.Parse(maxSkill.text);
            CoreSystem.defaultOpenAvail = int.Parse(defaultOpen.text);
            CoreSystem.defaultCloseAvail = int.Parse(defaultClose.text);
            CoreSystem.positionList = positionList;
            Destroy(this.gameObject);
        }
    }
}