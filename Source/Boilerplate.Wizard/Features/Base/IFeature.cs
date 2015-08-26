namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;

    public interface IFeature
    {
        string Title { get; }

        string Description { get; }

        string GroupName { get; }

        int Order { get; }

        Task AddOrRemoveFeature();
    }
}
