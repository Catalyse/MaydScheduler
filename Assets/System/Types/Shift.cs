using UnityEngine;
using System;
using System.Collections;
using CoreSys.Employees;

namespace CoreSys
{
    public class Shift//This should be expanded to hold more data
    {
        public Employee employee;
        public int startShift, endShift;
        public DateTime date;
        
        public Shift() { }
        
        public Shift(Employee emp, int start, int end, DateTime d)//may need to float these or something to handle non hourly incremental shifts 
        {
            employee = emp;
            startShift = start;
            endShift = end;
            DateTime date = d;
        }
    }
}
