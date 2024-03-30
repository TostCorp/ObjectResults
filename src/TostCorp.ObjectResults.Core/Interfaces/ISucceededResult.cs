using System.Collections.ObjectModel;

namespace TostCorp.ObjectResults.Core.Interfaces;

public interface ISucceededResult : IObjectResult
{
    public ReadOnlyCollection<ISuccess> Successes { get; }
}

public interface ISucceededResult<T> : ISucceededResult, IObjectResult<T>;
