namespace MvcBoilerplate.Commands
{
    using Boilerplate.AspNetCore;
    using MvcBoilerplate.ViewModels;

    public interface IPostCarCommand : IAsyncCommand<SaveCar>
    {
    }
}
