namespace ApiTemplate.Commands
{
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;

    public interface IPostCarCommand : IAsyncCommand<SaveCar>
    {
    }
}
