namespace Boilerplate.Wizard.Features
{
    using System.ComponentModel;

    public interface IMultiChoiceFeature : IFeature
    {
        FeatureItemCollection Items { get; }

        ICollectionView ItemsView { get; }
    }
}
