using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys
{
    public class LoadingSpin : MonoBehaviour
    {
        public Image loadingImage;
        public Text loadingText;
        private bool lEnabled = false;
        private float speed = 500;

        public void Update()
        {
            if (CoreSystem.currentlyProcessing == true && lEnabled == false)
                lEnabled = true;
            else if (CoreSystem.currentlyProcessing == false && lEnabled == true)
                lEnabled = false;
            LoadingProcess();
        }

        private void LoadingProcess()
        {
            if (lEnabled)
            {
                loadingImage.gameObject.transform.Rotate(((Vector3.forward * speed) * Time.deltaTime), Space.World);//rotate effect
                Color alpha = loadingImage.color;
                if (alpha.a < 1f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 1f, Time.deltaTime * 2);//fade out
                }
                loadingImage.color = alpha;
                alpha = loadingText.color;
                if (alpha.a < 1f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 1f, Time.deltaTime * 2);//fade out
                }
                loadingText.color = alpha;
            }
            else
            {
                Color alpha = loadingImage.color;
                if (alpha.a > 0f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 0f, Time.deltaTime * 2);//fade in
                }
                loadingImage.color = alpha;
                alpha = loadingText.color;
                if (alpha.a > 0f)
                {
                    alpha.a = Mathf.Lerp(alpha.a, 0f, Time.deltaTime * 2);//fade in
                }
                loadingText.color = alpha;
            }
        }
    }
}