using UnityEngine;
using System.Collections;

namespace CoreSys
{
    public class Window : MonoBehaviour
    {
        private WindowManagement manager;
        public void Awake()
        {
            manager = transform.root.GetComponent<WindowManagement>();
        }
        public void OnEnable()
        {
            manager.WindowActivated(this);
        }
        public void OnDisable()
        {
            manager.WindowDeactivated(this);
        }
        public void CloseWindow()
        {
            Destroy(this.transform.gameObject);
        }
        public void MoveWindow()
        {

        }
    }
}