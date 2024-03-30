using System.Net;
using System.Text.Json.Serialization;

using TostCorp.ObjectResults.Core.Interfaces;

namespace TostCorp.ObjectResults.Core.Types.Success;

public sealed record StatusCodeSuccessResult(HttpStatusCode StatusCode) : IPredefinedReason, ISuccess
{
    [JsonIgnore]
    public string? Name { get; init; }
}
