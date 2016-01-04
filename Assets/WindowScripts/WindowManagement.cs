using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Windows;
using CoreSys.Employees;

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
            EmployeeStorage.Start();
        }

        public void OnDisable()
        {
            systemActive = false;
        }

        public void OnDestroy()
        {
            EmployeeStorage.OnDestroy();
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
                WindowInstantiator.SpawnWindow(prefabs.prefabList[0]);
            }
        }
    }
}
