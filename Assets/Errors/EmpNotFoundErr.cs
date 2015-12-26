using UnityEngine;
using System.Collections;

namespace CoreSys.Errors
{
    public class EmpNotFoundErr : System.Exception
    {
        private string genericError = "Employee Not Found Error: The Employee does not exist!";
        private string directedError = "";

        public EmpNotFoundErr() { }

        public EmpNotFoundErr(string msg)
        {
            directedError = "Employee Not Found: " + msg;
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