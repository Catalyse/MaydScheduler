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
        public string empLastName, empFirstName;
        public int empID;
        public bool solutions;//solutions if true, exp if false
        public bool fulltime;//If true, fulltime, else parttime
        public bool active;
        public Day sunday, monday, tuesday, wednesday, thursday, friday, saturday;

        public Employee() { }//Default for serialization

        public void SetEmployee(string lastName, int id, bool _solutions, bool _fulltime, bool sun, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat)
        {
            active = true;
            empLastName = lastName;
            empID = id;
            solutions = _solutions;
            fulltime = _fulltime;
            sunday = new Day(sun);
            monday = new Day(mon);
            tuesday = new Day(tue);
            wednesday = new Day(wed);
            thursday = new Day(thu);
            friday = new Day(fri);
            saturday = new Day(sat);
        }
    }
}