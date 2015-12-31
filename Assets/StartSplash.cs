using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys
{
    /// <summary>
    /// Meant to handle splash screen fade
    /// </summary>
    public class StartSplash : MonoBehaviour
    {
        public Text t1, t2, t3;
        public Image i1;

        public void Start()
        {
            t1.color = new Color(t1.color.r, t1.color.g, t1.color.b, 50);
            t2.color = new Color(t2.color.r, t2.color.g, t2.color.b, 50);
            t3.color = new Color(t3.color.r, t3.color.g, t3.color.b, 50);
            i1.color = new Color(i1.color.r, i1.color.g, i1.color.b, 50);
        }

        public void Update()
        {
            float newAlpha = Mathf.Lerp(t1.color.a, 0, 2 * Time.deltaTime);
            t1.color = new Color(t1.color.r, t1.color.g, t1.color.b, newAlpha);
            newAlpha = Mathf.Lerp(t2.color.a, 0, 2 * Time.deltaTime);
            t2.color = new Color(t2.color.r, t2.color.g, t2.color.b, newAlpha);
            newAlpha = Mathf.Lerp(t3.color.a, 0, 2 * Time.deltaTime);
            t3.color = new Color(t3.color.r, t3.color.g, t3.color.b, newAlpha);
            newAlpha = Mathf.Lerp(i1.color.a, 0, 2 * Time.deltaTime);
            i1.color = new Color(i1.color.r, i1.color.g, i1.color.b, newAlpha);
            if (t1.color.a < .01 && t2.color.a < .01 && t3.color.a < .01 && i1.color.a < .01)
            {
                t1.gameObject.SetActive(false);
                t2.gameObject.SetActive(false);
                t3.gameObject.SetActive(false);
                i1.gameObject.SetActive(false);
            }
        }
    }
}