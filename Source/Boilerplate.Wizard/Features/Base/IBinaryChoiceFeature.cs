namespace Boilerplate.Wizard.Features
{
    public interface IBinaryChoiceFeature : IFeature
    {
        string Icon { get; }

        bool IsSelected { get; set; }
    }
}
