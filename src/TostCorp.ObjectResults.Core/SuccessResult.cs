using System.Collections.ObjectModel;

using TostCorp.CollectionExtensions;
using TostCorp.ObjectResults.Core.Interfaces;
using TostCorp.ObjectResults.Core.Types.Success;

namespace TostCorp.ObjectResults.Core;

public class SuccessResult : ISucceededResult
{
    public SuccessResult()
    {
        Reasons = [];
    }

    public SuccessResult(Collection<IReason> reasons)
    {
        Reasons = reasons;
    }

    public ReadOnlyCollection<ISuccess> Successes => Reasons.FindAll(p => p is ISuccess).ConvertAll(p => (p as ISuccess)!).AsReadOnly();

    public Collection<IReason> Reasons { get; }
    public bool IsFailed => Reasons.Exists(p => p is IError);

    public bool IsSuccess => !IsFailed;

    public SuccessResult<T> WithValue<T>(T value)
    {
        return new SuccessResult<T>(value, Reasons);
    }
}

public class SuccessResult<T> : ISucceededResult<T>
{
    public SuccessResult(T value)
    {
        Reasons = [new ValueSuccessResult<T>(value)];
        Value = (Reasons.Find(p => p is ValueSuccessResult<T>) as ValueSuccessResult<T>)!.Value;
    }

    public SuccessResult(T value, Collection<IReason> reasons)
    {
        ArgumentNullException.ThrowIfNull(reasons);

        Reasons = reasons;
        reasons.Add(new ValueSuccessResult<T>(value));

        Value = (Reasons.Find(p => p is ValueSuccessResult<T>) as ValueSuccessResult<T>)!.Value;
    }

    public ReadOnlyCollection<ISuccess> Successes => Reasons.FindAll(p => p is ISuccess).ConvertAll(p => (p as ISuccess)!).AsReadOnly();
    public Collection<IReason> Reasons { get; }
    public T Value { get; }
    public bool IsFailed => Reasons.Exists(p => p is IError);

    public bool IsSuccess => !IsFailed;
}
