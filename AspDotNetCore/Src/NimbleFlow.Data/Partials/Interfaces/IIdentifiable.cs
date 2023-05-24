namespace NimbleFlow.Data.Partials.Interfaces;

public interface IIdentifiable<T>
{
    public T Id { get; set; }
}