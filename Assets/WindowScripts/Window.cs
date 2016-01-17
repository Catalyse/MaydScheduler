using UnityEngine;
using System.Collections;

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
            if(!CanvasSetWindow)
                manager.WindowActivated(this);
        }
        public void CloseWindow()
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