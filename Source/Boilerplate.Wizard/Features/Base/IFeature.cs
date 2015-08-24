namespace Boilerplate.Wizard.Features
{
    public interface IFeature
    {
        string Title { get; }

        string Description { get; }

        string GroupName { get; }

        int Order { get; }

        void AddOrRemoveFeature();
    }
}
