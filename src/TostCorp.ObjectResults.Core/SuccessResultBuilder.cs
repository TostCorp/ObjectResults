using TostCorp.ObjectResults.Core.Types.Success;

namespace TostCorp.ObjectResults.Core;

public static class SuccessResultBuilder
{
    public static SuccessResult<T> WithPagedData<T>(this SuccessResult<T> result, int page, int pageSize, int totalCount)
    {
        ArgumentNullException.ThrowIfNull(result);

        result.Reasons.Add(new PagedSuccessResult(page, pageSize, totalCount));

        return result;
    }

    public static SuccessResult<T> WithStatusCode<T>(this SuccessResult<T> result, System.Net.HttpStatusCode statusCode)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(statusCode);

        result.Reasons.Add(new StatusCodeSuccessResult(statusCode));

        return result;
    }

    public static SuccessResult WithStatusCode(this SuccessResult result, System.Net.HttpStatusCode statusCode)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(statusCode);

        result.Reasons.Add(new StatusCodeSuccessResult(statusCode));

        return result;
    }
}
