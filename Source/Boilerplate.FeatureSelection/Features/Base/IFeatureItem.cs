namespace Boilerplate.FeatureSelection.Features
{
    public interface IFeatureItem
    {
        string Id { get; }

        string Title { get; }

        string Description { get; }

        string Icon { get; }

        bool IsContributionWanted { get; }

        bool IsSelected { get; set; }

        int Order { get; }

        IFeature Feature { get; }

        string CommentName { get; }
    }
}
