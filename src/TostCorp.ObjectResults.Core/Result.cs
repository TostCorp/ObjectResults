using System.Collections.ObjectModel;

using TostCorp.CollectionExtensions;
using TostCorp.ObjectResults.Core.Interfaces;

namespace TostCorp.ObjectResults.Core;
public class Result : IObjectResult
{
    public Result()
    {
        Reasons = [];
    }

    public Result(Collection<IReason> reasons)
    {
        Reasons = reasons;
    }

    public bool IsFailed => Reasons.Exists(p => p is IError);

    public bool IsSuccess => !IsFailed;

    public Collection<IReason> Reasons { get; }

    public static SuccessResult Ok()
    {
        return new SuccessResult();
    }

    public static SuccessResult<T> Ok<T>(T value)
    {
        return new SuccessResult<T>(value);
    }

    public static FailureResult Fail()
    {
        return new FailureResult();
    }
}
