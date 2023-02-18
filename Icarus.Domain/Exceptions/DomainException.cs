namespace Icarus.Domain.Exceptions;

public class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public DomainException(string message)
        : base(message)
    {
    }
}