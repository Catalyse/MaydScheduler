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
        public string position;
        public int hourTarget;//If fulltime/parttime
        public bool active;
        public Availability availability = new Availability();//Holds 7 day week and hourly availability.
        //set if prior week had 3 close 2 open
        public bool priorWeek32;

        public Employee() { }//Default for serialization

        public void SetEmployee(string lastName, string firstName, int id, bool _solutions, bool _fulltime, bool sun, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat)
        {
            active = true;
            empLastName = lastName;
            empFirstName = firstName;
            empID = id;
            priorWeek32 = CoreSystem.RandomBool();
            solutions = _solutions;
            fulltime = _fulltime;
            availability.sunday = new Day(sun);
            availability.monday = new Day(mon);
            availability.tuesday = new Day(tue);
            availability.wednesday = new Day(wed);
            availability.thursday = new Day(thu);
            availability.friday = new Day(fri);
            availability.saturday = new Day(sat);
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
                    Debug.Log("Invalid case chosen! :: EmployeeManagement/Employee.cs :: GetAvailability(int day): Invalid Value for Day Thrown! :: Returning false!");
                    break;
            }
            return false;
        }

        //DEPRECIATED -- Get rid of this
        public void SetTempAvailability(Day avail, int day)
        {
            if (tempAvailability == null)
            {
                GenerateTempAvailability();
            }
            switch (day)
            {
                case 1:
                    tempAvailability.sunday = avail;
                    break;
                case 2:
                    tempAvailability.monday = avail;
                    break;
                case 3:
                    tempAvailability.tuesday = avail;
                    break;
                case 4:
                    tempAvailability.wednesday = avail;
                    break;
                case 5:
                    tempAvailability.thursday = avail;
                    break;
                case 6:
                    tempAvailability.friday = avail;
                    break;
                case 7:
                    tempAvailability.saturday = avail;
                    break;
            }
        }
        
        //DEPRECIATED -- Get rid of this
        private void GenerateTempAvailability()
        {
            tempAvailability = availability;
        }

        //DEPRECIATED -- Get rid of this
        public void MakeTempAvailabilityPermanent()
        {
            availability = tempAvailability;
        }
    }
}
