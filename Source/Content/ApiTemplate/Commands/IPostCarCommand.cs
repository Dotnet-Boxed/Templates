namespace ApiTemplate.Commands
{
    using Boilerplate.AspNetCore;
    using ApiTemplate.ViewModels;

    public interface IPostCarCommand : IAsyncCommand<SaveCar>
    {
    }
}
