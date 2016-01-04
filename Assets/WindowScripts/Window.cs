using UnityEngine;
using System.Collections;

namespace CoreSys
{
    public class Window : MonoBehaviour
    {
        public WindowManagement managerSet;
        private static WindowManagement manager;
        public void Awake()
        {
            //This script is always stored on the canvas.
            if(managerSet)
                manager = managerSet;
        }
        public void OnEnable()
        {
            manager.WindowActivated(this);
        }
        public void CloseWindow()
        {
            manager.WindowDeactivated(this);
            Destroy(this.transform.gameObject);
        }
        public void MoveWindow()
        {

        }
    }
}