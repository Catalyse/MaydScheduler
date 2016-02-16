using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CoreSys.Windows
{
    public class WeekBar : MonoBehaviour
    {
        public ChooseWeek parent;
        public Text weekBarText;
        public int weekIndex; //This is the index in the list of the week this bar stands for

        public WeekBar() { }

        public void SetBar(ChooseWeek parent, int index, string barText)
        {
            weekIndex = index;
            this.parent = parent;
            weekBarText.text = barText;
        }
    }
}