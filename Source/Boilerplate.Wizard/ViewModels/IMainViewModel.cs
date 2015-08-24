namespace Boilerplate.Wizard.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Features;
    using Framework.UI.Input;

    public interface IMainViewModel
    {
        DelegateCommand AddOrRemoveFeaturesCommand { get; }

        ObservableCollection<IFeature> Features { get; }

        ICollectionView FeaturesView { get; }
    }
}
