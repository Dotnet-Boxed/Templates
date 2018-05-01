namespace ApiTemplate.Commands
{
    using Boilerplate.AspNetCore;
    using ApiTemplate.ViewModels;

    public interface IGetCarPageCommand : IAsyncCommand<PageOptions>
    {
    }
}
