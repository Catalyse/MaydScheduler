using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace CoreSys.SubSystem
{
    public class CurrentTime : MonoBehaviour
    {
        public Text timeText;

        public void Update()
        {
            timeText.text = DateTime.Now.ToString();
        }
    }
}