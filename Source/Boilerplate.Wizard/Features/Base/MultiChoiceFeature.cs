namespace Boilerplate.Wizard.Features
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using Boilerplate.Wizard.Services;
    using Framework.ComponentModel;

    public abstract class MultiChoiceFeature : Feature, IMultiChoiceFeature
    {
        private readonly FeatureItemCollection items;
        private readonly ICollectionView itemsView;

        #region Constructors

        public MultiChoiceFeature(IProjectService projectService)
            : base(projectService)
        {
            this.items = new FeatureItemCollection(items);
            this.items.CollectionChanged += this.OnCollectionChanged;
            this.items.ItemChanged += this.OnItemChanged;
            this.itemsView = CollectionViewSource.GetDefaultView(this.items);
            this.itemsView.SortDescriptions.Add(new SortDescription(nameof(IFeatureItem.Order), ListSortDirection.Ascending));
        }

        public MultiChoiceFeature(IProjectService projectService, IEnumerable<IFeatureItem> items)
            : this(projectService)
        {
            this.Items.AddRange(items);
        }

        #endregion

        #region Public Properties

        public override bool IsDefaultSelected
        {
            get { return this.Items.IsDefaultSelected; }
        }

        public virtual bool IsMultiSelect { get; } = false;

        public FeatureItemCollection Items
        {
            get { return this.items; }
        }

        public ICollectionView ItemsView
        {
            get { return this.itemsView; }
        }

        #endregion

        #region Public Methods

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

        #endregion

        #region Protected Methods

        protected virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (FeatureItem featureItem in e.NewItems.OfType<FeatureItem>())
                {
                    featureItem.Feature = this;
                }
            }

            if (e.OldItems != null)
            {
                foreach (FeatureItem featureItem in e.NewItems.OfType<FeatureItem>())
                {
                    featureItem.Feature = null;
                }
            }
        }


        protected virtual void OnItemChanged(object sender, ItemChangedEventArgs<IFeatureItem> e)
        {
            this.OnPropertyChanged(nameof(IsDefaultSelected));
        }

        #endregion
    }
}
