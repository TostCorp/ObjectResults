using System.Collections.ObjectModel;

namespace TostCorp.ObjectResults.Core.Interfaces;

public interface IObjectResult
{
    public Collection<IReason> Reasons { get; }
    public bool IsFailed { get; }
    public bool IsSuccess { get; }
}

public interface IObjectResult<T> : IObjectResult
{
    public T Value { get; }
}
