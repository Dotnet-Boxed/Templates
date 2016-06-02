namespace Boilerplate.Wizard.Features
{
    using System.ComponentModel;

    public interface IMultiChoiceFeature : IFeature
    {
        bool IsMultiSelect { get; }

        FeatureItemCollection Items { get; }

        ICollectionView ItemsView { get; }
    }
}
