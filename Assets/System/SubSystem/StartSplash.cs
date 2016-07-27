using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CoreSys.Windows;

namespace CoreSys.Subsystem
{
    /// <summary>
    /// Meant to handle splash screen fade
    /// </summary>
    public class StartSplash : MonoBehaviour
    {
        public Text t1, t2, t3, loadingText, statusText;
        public Image i1, loadingImage;
        public GameObject mainMenu, retryPrompt;
        private bool cont = false;
        private bool auto = false;
        private bool lEnabled = false;
        private float speed = 500;
        private float retryWait = 5;//seconds
        private float timer = 0;
        private int retryCount = 1;

        public void Start()
        {
            t1.color = new Color(t1.color.r, t1.color.g, t1.color.b, 50);
            t2.color = new Color(t2.color.r, t2.color.g, t2.color.b, 50);
            t3.color = new Color(t3.color.r, t3.color.g, t3.color.b, 50);
            i1.color = new Color(i1.color.r, i1.color.g, i1.color.b, 50);
            lEnabled = true;
            NetworkIO.RequestBoth();
        }

        public void Update()
        {
            if (cont)
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
                    WindowInstantiator.SpawnWindow(mainMenu);
                    this.gameObject.SetActive(false);//Kill the empty to kill the rest
                }
            }
            if (CoreSystem.networkFailure)
            {
                loadingText.text = "Connection Failure";
                if (retryCount > 2)
                {
                    retryPrompt.SetActive(true);
                }
                else
                {
                    auto = true;
                    timer = retryWait;
                    CoreSystem.networkFailure = false;
                    retryCount++;
                }
            }
            if(auto)
            {
                AutoRetry();
            }
            else if (CoreSystem.networkLoading == false && CoreSystem.networkFailure == false)
            {
                cont = true;
                loadingText.text = "Connection Successful";
                lEnabled = false;
            }
            LoadingProcess();
        }

        private void AutoRetry()
        {
            if (timer > 0.0f)
            {
                timer = timer - Time.deltaTime;
                if (timer < retryWait * 0.8f)
                {
                    loadingText.text = "Waiting for Attempt: " + retryCount + " in: " + Mathf.CeilToInt(timer) + "...";
                }
            }
            else
            {
                loadingText.text = "Connection Attempt: " + retryCount;
                NetworkIO.RequestBoth();
                auto = false;
            }
        }
        
        public void TryAgain()
        {
            loadingText.text = "Retrying Connection";
            retryPrompt.SetActive(false);
            auto = true;
            retryCount = 1;
            CoreSystem.networkFailure = false;
        }

        public void DontTryAgain()
        {
            retryPrompt.SetActive(false);
            cont = true;
            lEnabled = false;
            CoreSystem.networkFailure = false;
        }

        private void LoadingProcess()
        {
            if (lEnabled)
            {
                loadingImage.gameObject.transform.Rotate(((Vector3.forward * speed) * Time.deltaTime), Space.World);//rotate effect
                Color alpha = loadingImage.color;
                if (alpha.a < 1f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 1f, Time.deltaTime * 2);//fade in
                }
                loadingImage.color = alpha;
                alpha = loadingText.color;
                if (alpha.a < 1f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 1f, Time.deltaTime * 2);//fade in
                }
                loadingText.color = alpha;
            }
            else
            {
                Color alpha = loadingImage.color;
                if (alpha.a > 0f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 0f, Time.deltaTime * 2);//fade out
                }
                loadingImage.color = alpha;
                alpha = loadingText.color;
                if (alpha.a > 0f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 0f, Time.deltaTime * 2);//fade out
                }
                loadingText.color = alpha;
                alpha = statusText.color;
                if (alpha.a > 0f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 0f, Time.deltaTime * 2);//fade out
                }
                statusText.color = alpha;
            }
        }
    }
}