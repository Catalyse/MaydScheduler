using UnityEngine;
using System.Collections;


namespace CoreSys.Errors
{
    /// <summary>
    /// Generic Employee list not found error message.
    /// </summary>
    public class EmpListNotFoundErr : System.Exception
    {
        private string genericError = "EmployeeList Not Found Error: The Employee List does not exist!";
        private string directedError = "";

        public EmpListNotFoundErr() { }

        public EmpListNotFoundErr(string msg)
        {
            directedError = "EmployeeList Not Found Error: " + msg;
        }

        public void ThrowMsg()
        {
            if (directedError != "")
            {
                Debug.Log(directedError);
            }
            else
                Debug.Log(genericError);
        }
    }
}