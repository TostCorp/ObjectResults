namespace TostCorp.ObjectResults.Core.Interfaces;

public interface IError : IReason
{
    public string? Message { get; init; }
}
