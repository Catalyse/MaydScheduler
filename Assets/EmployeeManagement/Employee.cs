using UnityEngine;
using System.Collections;

namespace CoreSys
{
    /// <summary>
    /// TODO Add hourly availability
    /// </summary>
    public class Employee
    {
        public string name;
        public int employeeID;
        public bool solutions;//solutions if true, exp if false
        public bool fulltime;//If true, fulltime, else parttime
        public bool sun, mon, tue, wed, thu, fri, sat;

        public Employee() { }

        public Employee(string _name, int _id, bool _solutions, bool _fulltime, bool _sun, bool _mon, bool _tue, bool _wed, bool _thu, bool _fri, bool _sat)
        {
            name = _name;
            employeeID = _id;
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