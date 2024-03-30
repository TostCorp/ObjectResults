using System.Collections.ObjectModel;

namespace TostCorp.ObjectResults.Core.Interfaces;

public interface IFailedResult : IObjectResult
{
    public ReadOnlyCollection<IError> Errors { get; }
}
