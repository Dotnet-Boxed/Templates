namespace Boilerplate.Wizard.Features
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using Boilerplate.Wizard.Services;
    using Framework.ComponentModel;

    public abstract class MultiChoiceFeature : NotifyPropertyChanges, IMultiChoiceFeature
    {
        private readonly IProjectService projectService;
        private readonly FeatureItemCollection items;
        private readonly ICollectionView itemsView;

        public MultiChoiceFeature(IProjectService projectService, IEnumerable<IFeatureItem> items)
        {
            this.projectService = projectService;
            this.items = new FeatureItemCollection(items);
            this.items.ItemChanged += this.OnItemChanged;
            this.itemsView = CollectionViewSource.GetDefaultView(this.items);
            this.itemsView.SortDescriptions.Add(new SortDescription(nameof(IFeatureItem.Order), ListSortDirection.Ascending));
        }

        public abstract string Description { get; }

        public abstract string GroupName { get; }

        public bool IsDefaultSelected
        {
            get { return this.Items.IsDefaultSelected; }
        }

        public FeatureItemCollection Items
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(this.Title);

            foreach (var item in this.Items)
            {
                stringBuilder.Append("    ");
                stringBuilder.AppendLine(item.ToString());
            }

            return stringBuilder.ToString();
        }

        private void OnItemChanged(object sender, ItemChangedEventArgs<IFeatureItem> e)
        {
            this.OnPropertyChanged(nameof(IsDefaultSelected));
        }
    }
}
