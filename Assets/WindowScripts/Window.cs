using UnityEngine;
using System.Collections;
using System;

namespace CoreSys
{
    public class Window : MonoBehaviour
    {
        public WindowManagement managerSet;
        public bool CanvasSetWindow = false;//Set this on the canvas window to prevent it from being added to the manager
        private static WindowManagement manager;
        public void Awake()
        {
            //This script is always stored on the canvas.
            if(CanvasSetWindow)
                manager = managerSet;
        }
        public void OnEnable()
        {
            try
            {
                if (!CanvasSetWindow)
                    manager.WindowActivated(this);
            }
            catch (NullReferenceException)
            {
                Debug.Log("Window cannot be initialized because manager has not been set. Window existed before scene started");
			}

        }
        public virtual void CloseWindow()
        {
            manager.WindowDeactivated(this);
            Destroy(this.transform.gameObject);
        }
        public void MoveWindow()
        {
            //TODO
        }
    }
}