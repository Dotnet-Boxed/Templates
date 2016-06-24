namespace Boilerplate.FeatureSelection.Features
{
    public interface IBinaryChoiceFeature : IFeature
    {
        string Icon { get; }

        bool IsSelected { get; set; }
    }
}
