namespace NimbleFlow.Data.Partials.Interfaces;

public interface IToDto<out TDto>
{
    public TDto ToDto();
}