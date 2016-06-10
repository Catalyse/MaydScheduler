using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CoreSys.Employees;

namespace CoreSys
{
    public class EmployeeScheduleWrapper
    {
        public string lName, fName;
        public int employee;
        public int position;
        public int maxHours;
        public int skill;//This is to minimize callbacks to the main emp later
        public int scheduledHours;
        //The availability is copied onto this so that it can be modified without effecting the set availability in the employee type
        public Availability availability = new Availability();
        public bool availabilityModified = false;
        public List<Shift> shiftList = new List<Shift>();

        public EmployeeScheduleWrapper() { }

        public EmployeeScheduleWrapper(Employee emp)
        {
            lName = emp.empLastName;
            fName = emp.empFirstName;
            employee = emp.empID;
            position = emp.position;
            skill = emp.skillLevel;
            maxHours = emp.hourTarget;
            scheduledHours = 0;
            //availability = emp.availability;//Dont init this unless its changed and needs to be saved
            availabilityModified = false;
        }

        public void SetTempAvailability(Availability avail)//TODO add check for when avail last changed to warn user if availability is no longer useful
        {
            availability = avail;
            availabilityModified = true;
        }

        public void PermanentAvailabilityChange(Availability avail)
        {
            availability = avail;
            availabilityModified = false;//since were using the permanent change
            EmployeeStorage.GetEmployee(employee).availability = avail;
        }

        public Availability GetEntireAvail()
        {
            if (availabilityModified)
                return availability;
            else
                return EmployeeStorage.GetEmployee(employee).availability;
        }

        public bool GetAvailability(int day)
        {
            if (availabilityModified)
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
            else
            {
                return EmployeeStorage.GetEmployee(employee).GetAvailability(day);
            }
        }
    }
}
