using UnityEngine;
using System.Collections;
using CoreSys.Types;

namespace CoreSys.Employees
{
    /// <summary>
    /// TODO Add hourly availability
    /// </summary>
    public class Employee
    {
        public string empName;
        public int empID;
        public bool solutions;//solutions if true, exp if false
        public bool fulltime;//If true, fulltime, else parttime
        public bool active;
        public Day sunday, monday, tuesday, wednesday, thursday, friday, saturday;

        public Employee() { }

        public void SetEmployee(string _name, int _id, bool _solutions, bool _fulltime, bool _sun, bool _mon, bool _tue, bool _wed, bool _thu, bool _fri, bool _sat)
        {
            active = true;
            empName = _name;
            empID = _id;
            solutions = _solutions;
            fulltime = _fulltime;
            sunday = new Day(_sun);
            monday = new Day(_mon);
            tuesday = new Day(_tue);
            wednesday = new Day(_wed);
            thursday = new Day(_thu);
            friday = new Day(_fri);
            saturday = new Day(_sat);
        }
    }
}