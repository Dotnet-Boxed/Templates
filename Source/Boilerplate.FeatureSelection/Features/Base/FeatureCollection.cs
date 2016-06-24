namespace Boilerplate.FeatureSelection.Features
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Framework.ComponentModel;

    public class FeatureCollection : ObservableItemsCollection<IFeature>
    {
        public FeatureCollection()
        {
        }

        public FeatureCollection(IEnumerable<IFeature> features)
            : base(features)
        {
        }

        public bool IsDefaultsSelected
        {
            get { return this.All(x => x.IsDefaultSelected); }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var feature in this)
            {
                stringBuilder.AppendLine(feature.ToString());
            }

            return stringBuilder.ToString();
        }

        protected override void OnItemChanged(ItemChangedEventArgs<IFeature> e)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsDefaultsSelected)));
            base.OnItemChanged(e);
        }
    }
}
