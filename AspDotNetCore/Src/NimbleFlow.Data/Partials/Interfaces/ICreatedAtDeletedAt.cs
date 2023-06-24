namespace NimbleFlow.Data.Partials.Interfaces;

public interface ICreatedAtDeletedAt
{
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}