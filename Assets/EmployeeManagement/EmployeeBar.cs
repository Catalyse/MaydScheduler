using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Windows
{
    public class EmployeeBar : MonoBehaviour
    {
        public Text BarText;
        private string name, title;
        private int id, status;
        public Toggle barToggle;

        public EmployeeBar()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">empname</param>
        /// <param name="id">empid</param>
        /// <param name="title"></param>
        /// <param name="status">0 for inactive, 1 for part time, 2 for fulltime</param>
        public EmployeeBar(string name, int id, string title, int status)
        {

        }

        private void SetBarText()
        {
            BarText.text = name + " || " + title + " || " + id + " || " + status;
           
        }
        private void SetBarColor()
        {
            //TODO
        }
    }
}