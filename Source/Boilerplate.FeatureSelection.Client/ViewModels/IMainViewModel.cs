namespace Boilerplate.FeatureSelection.ViewModels
{
    using System.ComponentModel;
    using Boilerplate.FeatureSelection.Features;
    using Framework.UI.Input;

    public interface IMainViewModel
    {
        AsyncDelegateCommand AddOrRemoveFeaturesCommand { get; }

        string Error { get; }

        FeatureCollection Features { get; }

        ICollectionView FeaturesView { get; }

        bool IsLoaded { get; }
    }
}
