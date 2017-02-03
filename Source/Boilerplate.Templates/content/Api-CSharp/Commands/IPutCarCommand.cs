namespace ApiTemplate.Commands
{
    using Boilerplate.AspNetCore;
    using ApiTemplate.ViewModels;

    public interface IPutCarCommand : IAsyncCommand<int, SaveCar>
    {
    }
}
