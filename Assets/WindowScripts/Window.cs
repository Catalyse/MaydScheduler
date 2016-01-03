using UnityEngine;
using System.Collections;

namespace CoreSys
{
    public class Window : MonoBehaviour
    {
        private WindowManagement manager;
        public void Awake()
        {
            //This script is always stored on the canvas.
            manager = transform.root.GetComponent<WindowManagement>();
            if (manager) { }
            else
                Debug.Log("WindowManager Not Found!");
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