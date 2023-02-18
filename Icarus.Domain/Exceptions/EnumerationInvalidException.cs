using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Domain.Exceptions;

public sealed class EnumerationInvalidException : Exception
{
    public EnumerationInvalidException(Type type)
        : base($"The type {type.Name} is not valid enumeration type.")
    {

    }
}
