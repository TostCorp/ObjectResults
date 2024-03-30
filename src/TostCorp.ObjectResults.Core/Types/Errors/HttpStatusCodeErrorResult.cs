using System.Net;
using System.Text.Json.Serialization;

using TostCorp.ObjectResults.Core.Interfaces;

namespace TostCorp.ObjectResults.Core.Types.Errors;

public sealed record HttpStatusCodeErrorResult(HttpStatusCode StatusCode) : IPredefinedReason, IError
{
    [JsonIgnore]
    public string? Message { get; init; }

    [JsonIgnore]
    public string? Name { get; init; }
}
