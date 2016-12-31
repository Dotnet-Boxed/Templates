namespace MvcBoilerplate.Commands
{
    using Boilerplate.AspNetCore;
    using MvcBoilerplate.ViewModels;

    public interface IPutCarCommand : IAsyncCommand<int, SaveCar>
    {
    }
}
