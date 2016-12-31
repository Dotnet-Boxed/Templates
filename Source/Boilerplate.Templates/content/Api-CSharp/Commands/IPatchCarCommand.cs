namespace MvcBoilerplate.Commands
{
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;
    using MvcBoilerplate.ViewModels;

    public interface IPatchCarCommand : IAsyncCommand<int, JsonPatchDocument<SaveCar>>
    {
    }
}
