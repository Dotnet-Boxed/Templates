namespace Boilerplate.FeatureSelection.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Data;
    using Autofac;
    using Boilerplate.FeatureSelection.Features;
    using Boilerplate.FeatureSelection.Services;
    using Framework.UI.Input;

    public class DesignMainViewModel : IMainViewModel
    {
        private readonly FeatureCollection features;
        private readonly ICollectionView featuresView;

        public DesignMainViewModel()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterServices(@"C:\");
            builder.RegisterFeatureSet(FeatureSet.Mvc6);
            var container = builder.Build();

            this.features = new FeatureCollection(container.Resolve<IEnumerable<IFeature>>());
            this.featuresView = CollectionViewSource.GetDefaultView(this.features);
            this.featuresView.Filter = x => ((IFeature)x).IsVisible;
            this.featuresView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(IFeature.Group) + "." + nameof(IFeatureGroup.Name)));
            this.featuresView.SortDescriptions.Add(new SortDescription(nameof(IFeature.Group) + "." + nameof(IFeatureGroup.Order), ListSortDirection.Ascending));
            this.featuresView.SortDescriptions.Add(new SortDescription(nameof(IFeature.Order), ListSortDirection.Ascending));
        }

        public AsyncDelegateCommand AddOrRemoveFeaturesCommand
        {
            get { return null; }
        }

        public string Error
        {
            get { return null; }
        }

        public FeatureCollection Features
        {
            get { return this.features; }
        }

        public ICollectionView FeaturesView
        {
            get { return this.featuresView; }
        }

        public bool IsLoaded
        {
            get { return true; }
        }
    }
}
