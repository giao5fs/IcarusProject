namespace Icarus.Domain.Exceptions;
using FluentValidation.Results;
public sealed class ValidationException : Exception
{
    public IDictionary<string, string[]> Failures { get; }
    public ValidationException() : base("One or more validation failures have occured.")
    {
        Failures = new Dictionary<string, string[]>();
    }

    public ValidationException(IReadOnlyCollection<ValidationFailure> failures) : this()
    {
        IEnumerable<string> propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (string propertyName in propertyNames)
        {
            string[] propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            Failures.Add(propertyName,propertyFailures);
        }
    }
}
