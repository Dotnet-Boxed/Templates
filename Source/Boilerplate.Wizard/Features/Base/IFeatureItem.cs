namespace Boilerplate.Wizard.Features
{
    public interface IFeatureItem
    {
        string Title { get; }

        string Description { get; }

        string Icon { get; }

        bool IsContributionWanted { get; }

        bool IsSelected { get; set; }

        int Order { get; }
    }
}
