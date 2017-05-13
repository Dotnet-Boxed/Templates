namespace Boilerplate.FeatureSelection.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Data;
    using Boilerplate.FeatureSelection.Features;
    using Boilerplate.FeatureSelection.Services;
    using Framework.ComponentModel;
    using Framework.UI.Input;

    public class MainViewModel : NotifyPropertyChanges, IMainViewModel
    {
        #region Fields

        private readonly FeatureCollection features;
        private readonly ICollectionView featuresView;
        private readonly IFileSystemService fileSystemService;
        private readonly AsyncDelegateCommand addOrRemoveFeaturesCommand;

        private string error;
        private bool isLoaded = true;

        #endregion

        #region Constructors

        public MainViewModel(IEnumerable<IFeature> features, IFileSystemService fileSystemService)
        {
            this.features = new FeatureCollection(features);
            this.fileSystemService = fileSystemService;

            this.featuresView = CollectionViewSource.GetDefaultView(this.features);
            this.featuresView.Filter = x => ((IFeature)x).IsVisible;
            this.featuresView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(IFeature.Group) + "." + nameof(IFeatureGroup.Name)));
            this.featuresView.SortDescriptions.Add(new SortDescription(nameof(IFeature.Group) + "." + nameof(IFeatureGroup.Order), ListSortDirection.Ascending));
            this.featuresView.SortDescriptions.Add(new SortDescription(nameof(IFeature.Order), ListSortDirection.Ascending));
            this.addOrRemoveFeaturesCommand = new AsyncDelegateCommand(this.AddOrRemoveFeatures);
        }

        #endregion

        #region Public Properties

        public AsyncDelegateCommand AddOrRemoveFeaturesCommand
        {
            get { return this.addOrRemoveFeaturesCommand; }
        }

        public string Error
        {
            get { return this.error; }
            private set { this.SetProperty(ref this.error, value); }
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
            get { return this.isLoaded; }
            private set { this.SetProperty(ref this.isLoaded, value); }
        }

        #endregion

        #region Private Methods

        private async Task AddOrRemoveFeatures()
        {
            try
            {
                this.IsLoaded = false;

                MessageBox.Show(
                    "The changes you selected will now be applied. Unfortunately, due to a Visual Studio 2017 bug, this will cause Visual Studio to crash. This is totally expected and you can just re-open the project using Visual Studio and continue as normal. We are looking at ways of fixing this issue.",
                    "ASP.NET Core Boilerplate Project",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                var exceptions = new List<Exception>();

                foreach (var feature in this.features)
                {
                    try
                    {
                        await feature.AddOrRemoveFeature();
                    }
                    catch (Exception exception)
                    {
                        exceptions.Add(exception);
                    }
                }

                try
                {
                    await this.fileSystemService.SaveAll();
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }

                if (exceptions.Count > 0)
                {
                    this.SetError(exceptions);
                }
            }
            finally
            {
                this.IsLoaded = true;
            }
        }

        private void SetError(IEnumerable<Exception> exceptions)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Features");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(this.Features.ToString());

            stringBuilder.AppendLine("Exceptions");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(new AggregateException(exceptions).ToString());

            this.Error = stringBuilder.ToString();
        }

        #endregion
    }
}
