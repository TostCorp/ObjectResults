using System.Net;

using TostCorp.CollectionExtensions;
using TostCorp.ObjectResults.Core.Interfaces;
using TostCorp.ObjectResults.Core.Types.Errors;

namespace TostCorp.ObjectResults.Core;

public static class FailureResultBuilder
{
    public static FailureResult WithType(this FailureResult result, string type)
    {
        ArgumentNullException.ThrowIfNull(result);

        result.WithError(new TypeErrorResult(type));

        return result;
    }

    public static FailureResult WithTitle(this FailureResult result, string title)
    {
        ArgumentNullException.ThrowIfNull(result);

        result.WithError(new TitleErrorResult(title));

        return result;
    }

    public static FailureResult WithException<T>(this FailureResult result, T exception, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) where T : Exception
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(exception);

        result.WithError(new TypeErrorResult(exception.GetType().Name));
        result.WithError(new HttpStatusCodeErrorResult(statusCode));
        result.WithError(new TitleErrorResult(exception.Source));
        result.WithError(new ReasonErrorResult(exception.Message));

        return result;
    }

    public static FailureResult WithHttpStatusCode(this FailureResult result, HttpStatusCode statusCode)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (!Constants.StatusCodeDefaults.TryGetValue((int)statusCode, out var statusCodeConstant))
        {
            _ = Constants.StatusCodeDefaults.TryGetValue((int)HttpStatusCode.InternalServerError, out statusCodeConstant);
        }

        result.WithError(new TitleErrorResult(statusCodeConstant.Title));
        result.WithError(new TypeErrorResult(statusCodeConstant.Type));
        result.WithError(new HttpStatusCodeErrorResult(statusCode));

        return result;
    }

    public static FailureResult WithReason(this FailureResult result, string reason)
    {
        return result.WithError(new ReasonErrorResult(reason));
    }

    public static FailureResult WithError(this FailureResult result, IError error)
    {
        ArgumentNullException.ThrowIfNull(result);

        result.Reasons.Add(error);
        return result;
    }

    public static ProblemDetails ToProblemDetails(this FailureResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        var detail = (result.Errors.LastOrDefault(p => p is ReasonErrorResult) as ReasonErrorResult)?.Message;
        var statusCode = (result.Errors.LastOrDefault(p => p is HttpStatusCodeErrorResult) as HttpStatusCodeErrorResult)?.StatusCode;
        var title = (result.Errors.LastOrDefault(p => p is TitleErrorResult) as TitleErrorResult)?.Message ?? "Unknown";
        var type = (result.Errors.LastOrDefault(p => p is TypeErrorResult) as TypeErrorResult)?.Message;
        var messages = result.Reasons.FindAll(p => p is not IPredefinedReason);

        var problemDetail = new ProblemDetails
        {
            Detail = detail ?? title,
            Status = (int?)statusCode ?? (int)HttpStatusCode.BadRequest,
            Title = title,
        };

        problemDetail.Type = type ?? $"https://httpstatuses.com/{problemDetail.Status}";

        foreach (var message in messages)
        {
            var name = !string.IsNullOrWhiteSpace(message.Name) ? message.Name : message.GetType().Name;
            _ = problemDetail.Extensions.TryAdd(name, message);
        }

        return problemDetail;
    }
}
