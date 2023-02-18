using Icarus.Domain.Shared;

namespace Icarus.Domain.Extensions;

public static class ResultExtension
{
    public static Result<T> ToResult<T>(this T? value, Error error) where T : class
    {
        return value is null ? Result.Failure<T>(error) : Result.Success(value);
    }

    public static Result<TOutputResult> OnSuccess<TCurrentResult, TOutputResult>
        (this Result<TCurrentResult> result, Func<TCurrentResult?, TOutputResult?> onSuccessFunc)
        where TCurrentResult : class
        where TOutputResult : class
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (onSuccessFunc is null)
        {
            return Result.Failure<TOutputResult>(new Error("onSuccessFuncNull", "The on success function cannot be null."));
        }

        return result.IsSuccess ? Result.Success(onSuccessFunc(result.Value)!) :
            Result.Failure<TOutputResult>(result.Error);
    }

    public static Result<T> Ensure<T>(this Result<T> result, Func<T?, bool> predicate, Error error)
        where T : class
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (predicate is null)
        {
            return Result.Failure<T>(new Error("", "The predicate cannot be null"));
        }

        if (result.IsFailure)
        {
            return result;
        }

        return predicate(result.Value) ? result : Result.Failure<T>(error);
    }

    public static Result<T> Map<T>(this Result result, Func<Result<T>> func)
        => result.IsSuccess ? func() : Result.Failure<T>(result.Error);

    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn?, TOut?> mapFunc)
        where TIn : class
        where TOut : class
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (mapFunc is null)
        {
            return Result.Failure<TOut>(new Error("", "The map function cannot be null"));
        }

        return result.IsSuccess ? Result.Success(mapFunc(result.Value)!) : Result.Failure<TOut>(result.Error);
    }
}
