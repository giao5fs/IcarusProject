namespace Icarus.Domain.Shared;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public Error Error { get; }

    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue val)
        => new(val, true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default(TValue), false, error);
    }

    public static Result<TValue> Create<TValue>(TValue val)
    {
        return new Result<TValue>(val, true, Error.None);
    }

}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) => _value = value;

    public TValue Value => IsSuccess ? _value! :
        throw new InvalidOperationException("The value of a failure result can not be accepted");

    public static implicit operator Result<TValue>(TValue? val) => Create(val)!;

}
