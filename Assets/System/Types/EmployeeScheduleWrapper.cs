using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;

namespace CoreSys
{
    public class EmployeeScheduleWrapper
    {
        public Employee employee;
        public int maxHours;
        public int scheduledHours;
        //The availability is copied onto this so that it can be modified without effecting the set availability in the employee type
        public Availability availability = new Availability();
        public bool availabilityModified = false;
        public List<Shift> shiftList = new List<Shift>();

        public EmployeeScheduleWrapper() { }

        public EmployeeScheduleWrapper(Employee emp)
        {
            employee = emp;
            maxHours = emp.hourTarget;
            scheduledHours = 0;
            availability = emp.availability;
            availabilityModified = false;
        }

        public void SetTempAvailability(Availability avail)
        {
            availability = avail;
            availabilityModified = true;
        }

        public void PermanentAvailabilityChange(Availability avail)
        {
            availability = avail;
            availabilityModified = true;
            employee.availability = avail;
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
    }
}