using System.Collections.ObjectModel;

using TostCorp.CollectionExtensions;
using TostCorp.ObjectResults.Core.Interfaces;

namespace TostCorp.ObjectResults.Core;

public class FailureResult : IFailedResult
{
    public FailureResult()
    {
        Reasons = [];
    }

    public ReadOnlyCollection<IError> Errors => Reasons.FindAll(p => p is IError).ConvertAll(p => (p as IError)!).AsReadOnly();

    public Collection<IReason> Reasons { get; }
    public bool IsFailed => Reasons.Exists(p => p is IError);

    public bool IsSuccess => !IsFailed;
}
