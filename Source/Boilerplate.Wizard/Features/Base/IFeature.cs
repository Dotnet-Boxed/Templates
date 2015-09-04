namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;

    public interface IFeature
    {
        string Id { get; }

        string Title { get; }

        string Description { get; }

        IFeatureGroup Group { get; }

        int Order { get; }

        bool IsDefaultSelected { get; }

        Task AddOrRemoveFeature();
    }
}
