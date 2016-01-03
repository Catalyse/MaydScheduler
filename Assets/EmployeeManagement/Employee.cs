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
        public Day sun, mon, tue, wed, thu, fri, sat;

        public Employee() { }

        public Employee(string _name, int _id, bool _solutions, bool _fulltime, Day _sun, Day _mon, Day _tue, Day _wed, Day _thu, Day _fri, Day _sat)
        {
            empName = _name;
            empID = _id;
            solutions = _solutions;
            fulltime = _fulltime;
            sun = _sun;
            mon = _mon;
            tue = _tue;
            wed = _wed;
            thu = _thu;
            fri = _fri;
            sat = _sat;
        }
    }
}