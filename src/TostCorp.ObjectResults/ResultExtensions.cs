using Microsoft.AspNetCore.Mvc;

using OneOf;

using System.Net;

using TostCorp.CollectionExtensions;
using TostCorp.ObjectResults.Core;
using TostCorp.ObjectResults.Core.Types.Success;

namespace TostCorp.ObjectResults;

public static class ResultExtensions
{
    public static IActionResult HandleResult(SuccessResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        
        return HandleSuccessResult(result);
    }

    public static IActionResult HandleResult<T>(SuccessResult<T> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return HandleTypedSuccessResult(result);
    }

    public static IActionResult HandleResult(FailureResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return HandleFailureResult(result);
    }

    public static IActionResult HandleResult(OneOf<SuccessResult, FailureResult> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Match(
            HandleSuccessResult,
            HandleFailureResult
            );
    }

    public static IActionResult HandleResult<T>(OneOf<SuccessResult<T>, FailureResult> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Match(
            HandleTypedSuccessResult,
            HandleFailureResult
            );
    }

    public static IActionResult HandleResult<T>(OneOf<SuccessResult<T>, SuccessResult, FailureResult> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Match(
            HandleTypedSuccessResult,
            HandleSuccessResult,
            HandleFailureResult
            );
    }

    private static IActionResult HandleSuccessResult(SuccessResult result)
    {
        var statusCodeResult = result.Successes.Find(static p => p is StatusCodeSuccessResult) as StatusCodeSuccessResult;
        var statusCode = statusCodeResult?.StatusCode ?? HttpStatusCode.NoContent;

        return new StatusCodeResult((int)statusCode);
    }

    private static ObjectResult HandleTypedSuccessResult<T>(SuccessResult<T> result)
    {
        var objectResult = result.Reasons.Find(static p => p is StatusCodeSuccessResult) as StatusCodeSuccessResult;
        var statusCode = objectResult?.StatusCode ?? HttpStatusCode.OK;

        return new ObjectResult(result.Value) { StatusCode = (int?)statusCode };
    }

    private static ObjectResult HandleFailureResult(FailureResult result)
    {
        var problemDetails = result.ToProblemDetails();

        return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
    }
}
