using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TestApplicationDomain.Validators
{
    public static class BaseValidation
    {
        public static bool IsValidEmail(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            return Regex.IsMatch(email, expression) && Regex.Replace(email, expression, string.Empty).Length == 0;
        }
    }
}
