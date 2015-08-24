namespace Boilerplate.Wizard.Features
{
    using Framework.ComponentModel;

    public class FeatureItem : NotifyPropertyChanges, IFeatureItem
    {
        private readonly string description;
        private readonly string icon;
        private readonly int order;
        private readonly string title;

        private bool isSelected;

        public FeatureItem(string title, string description, int order, string icon = null)
        {
            this.title = title;
            this.description = description;
            this.icon = icon;
            this.order = order;
        }

        public string Description
        {
            get { return this.description; }
        }

        public string Icon
        {
            get { return this.icon; }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetProperty(ref this.isSelected, value); }
        }

        public int Order
        {
            get { return this.order; }
        }

        public string Title
        {
            get { return this.title; }
        }
    }
}
