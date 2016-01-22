using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public class SetAllButton : MonoBehaviour
    {
        public InputField setField;
        public List<InputField> fieldList = new List<InputField>();

        public void Start()
        {
            InputField[] tempFieldList = GetComponentsInChildren<InputField>();
            foreach (InputField temp in tempFieldList)
            {
                fieldList.Add(temp);
            }
        }

        public void ButtonClicked()
        {
            for (int i = 0; i < fieldList.Count; i++)
            {
                InputField temp = fieldList[i];
                temp.text = setField.text;
            }
        }
    }
}