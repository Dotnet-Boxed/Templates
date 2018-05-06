namespace ApiTemplate.Commands
{
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;

    public interface IPutCarCommand : IAsyncCommand<int, SaveCar>
    {
    }
}
