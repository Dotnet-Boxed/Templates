namespace MvcBoilerplate.Commands
{
    using Boilerplate.AspNetCore;
    using MvcBoilerplate.ViewModels;

    public interface IGetCarPageCommand : IAsyncCommand<PageRequest>
    {
    }
}
