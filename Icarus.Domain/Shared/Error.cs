namespace Icarus.Domain.Shared;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
    
    public string Code { get; }
    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;
    
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Code == b.Code;
    }
    public static bool operator !=(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Code != b.Code;
    }
    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Code == Code;
    }

    public override bool Equals(object obj) => Equals(obj as Error);
}
