using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
	public class SystemMessages : MonoBehaviour
	{
        public float messageTime;//This is the time each message will be displayed
		public static List<string> messageList = new List<string>();
        private float timer = 0;//This is the current message timer
        public static bool queueActive = false;
        public Text currentMessage;

		public void Update()
		{
            if(queueActive)
            {
                if(timer <= 0)//if the current timer runs out
                {
                    currentMessage.text = "";
                    if (messageList.Count > 0)//we check if there are any more messages
                    {
                        timer = messageTime;
                        currentMessage.text = messageList[0];
                        messageList.RemoveAt(0);
                    }
                    else
                        queueActive = false;
                }
                else
                {
                    timer = timer - Time.deltaTime;
                }
            }
		}

        public static void ThrowMessage(string message)
        {
            queueActive = true;
            messageList.Add(message);
        }
	}
}