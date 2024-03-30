using System.Text.Json.Serialization;

using TostCorp.ObjectResults.Core.Interfaces;

namespace TostCorp.ObjectResults.Core.Types.Success;

public sealed record PagedSuccessResult(int PageNumber, int PageSize, int TotalCount) : ISuccess
{
    [JsonIgnore]
    public string? Name { get; init; }
}
