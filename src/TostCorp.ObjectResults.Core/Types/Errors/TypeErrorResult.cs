using System.Text.Json.Serialization;

using TostCorp.ObjectResults.Core.Interfaces;

namespace TostCorp.ObjectResults.Core.Types.Errors;

public sealed record TypeErrorResult(string? Message) : IPredefinedReason, IError
{
    [JsonIgnore]
    public string? Name { get; init; }
}
