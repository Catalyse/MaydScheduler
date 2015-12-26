using UnityEngine;
using System.Collections;


namespace CoreSys.Errors
{
    /// <summary>
    /// Generic Employee list not found error message.
    /// </summary>
    public class EmpListNotFoundErr
    {
        public EmpListNotFoundErr()
        {
            Debug.Log("EmployeeList Not Found Error: The Employee List does not exist!");
        }

        public EmpListNotFoundErr(string msg)
        {
            Debug.Log("EmployeeList Not Found Error: " + msg);
        }
    }
}