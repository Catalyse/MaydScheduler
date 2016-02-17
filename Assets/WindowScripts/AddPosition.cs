using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CoreSys;

namespace CoreSys.Windows
{
    public class AddPosition : Window
    {
        public SystemConfig parent;
        public InputField posText;

        public void Submit()
        {
            parent.positionList.Add(parent.positionList.Count, posText.text);
            posText.text = "";
            this.gameObject.SetActive(false);
        }
    }
}