namespace Boilerplate.Wizard.Features
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public interface IMultiChoiceFeature : IFeature
    {
        ObservableCollection<IFeatureItem> Items { get; }

        ICollectionView ItemsView { get; }
    }
}
