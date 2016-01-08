using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
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
        //set if prior week had 3 close 2 open
        public bool priorWeek32;

        public Employee() { }//Default for serialization

        public void SetEmployee(string lastName, string firstName, int id, bool _solutions, bool _fulltime, bool sun, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat)
        {
            active = true;
            empLastName = lastName;
            empFirstName = firstName;
            empID = id;
            priorWeek32 = RandomBool();
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

        public bool GetAvailability(int day)
        {
            switch (day)
            {
                case 0:
                    if (sunday.available)
                        return true;
                    else
                        return false;
                case 1:
                    if (monday.available)
                        return true;
                    else
                        return false;
                case 2:
                    if (tuesday.available)
                        return true;
                    else
                        return false;
                case 3:
                    if (wednesday.available)
                        return true;
                    else
                        return false;
                case 4:
                    if (thursday.available)
                        return true;
                    else
                        return false;
                case 5:
                    if (friday.available)
                        return true;
                    else
                        return false;
                case 6:
                    if (saturday.available)
                        return true;
                    else
                        return false;
                default:
                    Debug.Log("Invalid case chosen! :: EmployeeManagement/Employee.cs :: GetAvailability(int day): Invalid Value for Day Thrown! :: Returning false!");
                    break;
            }
            return false;
        }

        private bool RandomBool()
        {
            System.Random gen = new System.Random();
            int prob = gen.Next(100);
            if (prob < 50)
                return true;
            else
                return false;
        }
    }
}