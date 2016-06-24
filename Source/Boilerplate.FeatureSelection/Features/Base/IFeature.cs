namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;

    public interface IFeature
    {
        string Id { get; }

        bool IsDefaultSelected { get; }

        bool IsVisible { get; }

        string Title { get; }

        string Description { get; }

        IFeatureGroup Group { get; }

        int Order { get; }

        Task AddOrRemoveFeature();
    }
}
