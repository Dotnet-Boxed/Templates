namespace Boilerplate.Wizard.Features
{
    public interface IFeatureItem
    {
        string Title { get; }

        string Description { get; }

        string Icon { get; }

        int Order { get; }

        bool IsSelected { get; set; }
    }
}
