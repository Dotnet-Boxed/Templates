namespace Boilerplate.Wizard.Features
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using Boilerplate.Wizard.Services;

    public abstract class MultiChoiceFeature : IMultiChoiceFeature
    {
        private readonly IProjectService projectService;
        private readonly ObservableCollection<IFeatureItem> items;
        private readonly ICollectionView itemsView;

        public MultiChoiceFeature(IProjectService projectService, IEnumerable<IFeatureItem> items)
        {
            this.projectService = projectService;
            this.items = new ObservableCollection<IFeatureItem>(items);
            this.itemsView = CollectionViewSource.GetDefaultView(this.items);
            this.itemsView.SortDescriptions.Add(new SortDescription(nameof(IFeatureItem.Order), ListSortDirection.Ascending));
        }

        public abstract string Description { get; }

        public abstract string GroupName { get; }

        public ObservableCollection<IFeatureItem> Items
        {
            get { return this.items; }
        }

        public ICollectionView ItemsView
        {
            get { return this.itemsView; }
        }

        public abstract int Order { get; }

        public abstract string Title { get; }

        protected IProjectService ProjectService
        {
            get { return this.projectService; }
        }

        public abstract Task AddOrRemoveFeature();
    }
}
