namespace NimbleFlow.Contracts.Interfaces;

public interface IToModel<out TModel>
{
    public TModel ToModel();
}