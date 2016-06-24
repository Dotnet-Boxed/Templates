namespace Boilerplate.FeatureSelection.Features
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Framework.ComponentModel;

    public class FeatureItemCollection : ObservableItemsCollection<IFeatureItem>
    {
        public FeatureItemCollection()
        {
        }

        public FeatureItemCollection(IEnumerable<IFeatureItem> featureItems)
            : base(featureItems)
        {
        }

        public bool IsDefaultSelected
        {
            get { return this.Items.First().IsSelected; }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var featureItem in this)
            {
                stringBuilder.AppendLine(featureItem.ToString());
            }

            return stringBuilder.ToString();
        }

        protected override void OnItemChanged(ItemChangedEventArgs<IFeatureItem> e)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsDefaultSelected)));
            base.OnItemChanged(e);
        }
    }
}