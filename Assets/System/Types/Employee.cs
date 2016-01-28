using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys.Employees
{
    /// <summary>
    /// TODO Add hourly availability
    /// </summary>
    public class Employee
    {
        public string empLastName, empFirstName;
        public int empID;
        public int position;
        public int hourTarget;//If fulltime/parttime
        public int shiftPreference;
        public float skillLevel;
        public bool active, overtimeAllowed;
        public Availability availability = new Availability();//Holds 7 day week and hourly availability.
        //set if prior week had 3 close 2 open
        //public bool priorWeek32;//Depreciated(Or at least im not using it for now because its biast)

        public Employee() { }//Default for serialization

        public void SetEmployee(string lastName, string firstName, int id, int pos, int targetHours, bool sun, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat)
        {
            active = true;
            overtimeAllowed = false;
            empLastName = lastName;
            empFirstName = firstName;
            empID = id;
            //priorWeek32 = CoreSystem.RandomBool();//Depreciated
            position = pos;
            hourTarget = targetHours;
            availability.sunday = new Day(sun);
            availability.monday = new Day(mon);
            availability.tuesday = new Day(tue);
            availability.wednesday = new Day(wed);
            availability.thursday = new Day(thu);
            availability.friday = new Day(fri);
            availability.saturday = new Day(sat);
            shiftPreference = 8;
        }
        
        public void SetEmployee(string lastName, string firstName, int id, int pos, int targetHours, bool sun, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, int shiftPref)
        {
            active = true;
            overtimeAllowed = false;
            empLastName = lastName;
            empFirstName = firstName;
            empID = id;
            //priorWeek32 = CoreSystem.RandomBool();//Depreciated
            position = pos;
            hourTarget = targetHours;
            availability.sunday = new Day(sun);
            availability.monday = new Day(mon);
            availability.tuesday = new Day(tue);
            availability.wednesday = new Day(wed);
            availability.thursday = new Day(thu);
            availability.friday = new Day(fri);
            availability.saturday = new Day(sat);
            shiftPreference = shiftPref;
        }

        public bool GetAvailability(int day)
        {
            switch (day)
            {
                case 0:
                    if (availability.sunday.available)
                        return true;
                    else
                        return false;
                case 1:
                    if (availability.monday.available)
                        return true;
                    else
                        return false;
                case 2:
                    if (availability.tuesday.available)
                        return true;
                    else
                        return false;
                case 3:
                    if (availability.wednesday.available)
                        return true;
                    else
                        return false;
                case 4:
                    if (availability.thursday.available)
                        return true;
                    else
                        return false;
                case 5:
                    if (availability.friday.available)
                        return true;
                    else
                        return false;
                case 6:
                    if (availability.saturday.available)
                        return true;
                    else
                        return false;
                default:
                    Debug.Log("Invalid case chosen! :: Employee.cs :: GetAvailability(int day): Invalid Value for Day Thrown! :: Returning false!");
                    break;
            }
            return false;
        }
    }
}
