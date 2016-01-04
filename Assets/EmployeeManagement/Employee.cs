using UnityEngine;
using System.Collections;
using CoreSys.Types;

namespace CoreSys.Employees
{
    /// <summary>
    /// TODO Add hourly availability
    /// </summary>
    public class Employee : MonoBehaviour
    {
        public string empName;
        public int empID;
        public bool solutions;//solutions if true, exp if false
        public bool fulltime;//If true, fulltime, else parttime
        public Day sunday, monday, tuesday, wednesday, thursday, friday, saturday;

        public Employee() { }

        public Employee(string _name, int _id, bool _solutions, bool _fulltime, bool _sun, bool _mon, bool _tue, bool _wed, bool _thu, bool _fri, bool _sat)
        {
            empName = _name;
            empID = _id;
            solutions = _solutions;
            fulltime = _fulltime;
            sunday.available = _sun;
            monday.available = _mon;
            tuesday.available = _tue;
            wednesday.available = _wed;
            thursday.available = _thu;
            friday.available = _fri;
            saturday.available = _sat;
        }
    }
}