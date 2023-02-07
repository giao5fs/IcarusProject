using Icarus.Domain.Primitives;
using Icarus.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Domain.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public const int MaxLength = 50;
        public FirstName(string val)
        {
            Value = val;
        }

        public string Value { get; }

        public static Result<FirstName> Create(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return Result.Failure<FirstName>(new Error(
                "FirstName.Empty",
                "FirstName is empty"
                ));
            }

            if (firstName.Length > MaxLength)
            {
                return Result.Failure<FirstName>(new Error(
                    "FirstName.TooLong",
                    "FirstName is too long"
                    ));
            }

            return new FirstName(firstName);
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
