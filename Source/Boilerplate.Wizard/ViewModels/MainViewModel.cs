namespace Boilerplate.Wizard.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using Features;
    using Framework.ComponentModel;
    using Framework.UI.Input;

    public class MainViewModel : NotifyPropertyChanges, IMainViewModel
    {
        private readonly ObservableCollection<IFeature> features;
        private readonly ICollectionView featuresView;
        private readonly DelegateCommand addOrRemoveFeaturesCommand;

        #region Constructors

        public MainViewModel(IEnumerable<IFeature> features)
        {
            this.features = new ObservableCollection<IFeature>(features);
            this.featuresView = CollectionViewSource.GetDefaultView(this.features);
            this.featuresView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(IFeature.GroupName)));
            this.featuresView.SortDescriptions.Add(new SortDescription(nameof(IFeature.Order), ListSortDirection.Ascending));
            this.addOrRemoveFeaturesCommand = new DelegateCommand(this.AddOrRemoveFeatures);
        } 

        #endregion

        #region Public Properties

        public DelegateCommand AddOrRemoveFeaturesCommand
        {
            get { return this.addOrRemoveFeaturesCommand; }
        }

        public ObservableCollection<IFeature> Features
        {
            get { return this.features; }
        }

        public ICollectionView FeaturesView
        {
            get { return this.featuresView; }
        } 

        #endregion

        #region Private Methods

        private void AddOrRemoveFeatures()
        {
            foreach (IFeature feature in this.features)
            {
                feature.AddOrRemoveFeature();
            }
        }

        #endregion
    }
}
