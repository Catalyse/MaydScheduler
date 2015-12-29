using UnityEngine;
using System.Collections;

namespace CoreSys
{
    public class Window : MonoBehaviour
    {
        public void CloseWindow()
        {
            Destroy(this.transform.gameObject);
        }
        public void MoveWindow()
        {

        }
    }
}