using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public class WindowManagement : MonoBehaviour
    {
        public List<Window> activeWindowList = new List<Window>();
        private int activeWindows;
        private bool systemActive;
        public PrefabList prefabs;

        public void Awake()
        {
            systemActive = true;
        }

        public void OnDisable()
        {
            systemActive = false;
        }

        public void OnDestroy()
        {
            systemActive = false;
        }

        public void WindowActivated(Window window)
        {
            if (activeWindowList.Contains(window) == false)
            {
                activeWindowList.Add(window);
                activeWindows++;
            }
        }

        public void WindowDeactivated(Window window)
        {
            if(activeWindowList.Remove(window))
                activeWindows--;
            CheckForEmptyScreen();
        }

        private void CheckForEmptyScreen()
        {
            if(activeWindows <= 0 && systemActive)
            {
                activeWindows = 0;
                GameObject weeklyConfig = (GameObject)Instantiate(prefabs.prefabList[0], new Vector3(0, 0, 0), Quaternion.identity);
                weeklyConfig.transform.SetParent(this.gameObject.transform);
                weeklyConfig.transform.localScale = new Vector3(1, 1, 1);//Moving transforms seems to screw with the scale, so its set in code for redundancy
                weeklyConfig.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
            }
        }
    }
}
