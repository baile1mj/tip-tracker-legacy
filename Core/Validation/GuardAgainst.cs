using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validation;

public static class GuardAgainst
{
    public static void NullOrEmptyString(string value, [CallerArgumentExpression("value")]string paramName = "")
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName, "String value cannot be null or empty.");
        }
    }

    public static void Null(object value, [CallerArgumentExpression("value")]string paramName = "")
    {
        if (value == null)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}