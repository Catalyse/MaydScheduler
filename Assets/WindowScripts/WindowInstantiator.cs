using UnityEngine;
using System.Collections;

namespace CoreSys.Windows
{
    public class WindowInstantiator : MonoBehaviour
    {
        public GameObject defaultParentSet;
        public static GameObject defaultParent;

        public void Start()
        {
            defaultParent = defaultParentSet;
        }

        public static GameObject SpawnWindow(Object window)
        {
            GameObject newWindow = (GameObject)Instantiate(window, new Vector3(0, 0, 0), Quaternion.identity);
            newWindow.transform.SetParent(defaultParent.transform);
            newWindow.transform.localScale = new Vector3(1, 1, 1);//Moving transforms seems to screw with the scale, so its set in code for redundancy
            newWindow.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);//Initial pos set to center screen, indiv scripts can change this later.
            return newWindow;
        }

        public static GameObject SpawnWindow(GameObject window, GameObject parentTransform)
        {
            GameObject newWindow = (GameObject)Instantiate(window, new Vector3(0, 0, 0), Quaternion.identity);
            newWindow.transform.SetParent(parentTransform.transform);
            newWindow.transform.localScale = new Vector3(1, 1, 1);//Moving transforms seems to screw with the scale, so its set in code for redundancy
            newWindow.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);//Initial pos set to center screen, indiv scripts can change this later.
            return newWindow;
        }

        public static GameObject SpawnWindow(Object window, GameObject parentTransform)
        {
            GameObject newWindow = (GameObject)Instantiate(window, new Vector3(0, 0, 0), Quaternion.identity);
            newWindow.transform.SetParent(parentTransform.transform);
            newWindow.transform.localScale = new Vector3(1, 1, 1);//Moving transforms seems to screw with the scale, so its set in code for redundancy
            newWindow.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);//Initial pos set to center screen, indiv scripts can change this later.
            return newWindow;
        }
    }
}