namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public abstract class BinaryFeature : Feature, IBinaryFeature
    {
        private readonly bool defaultIsSelected;
        private bool isSelected;

        public BinaryFeature(IProjectService projectService, bool defaultIsSelected = true)
            : base(projectService)
        {
            this.defaultIsSelected = defaultIsSelected;
            this.isSelected = defaultIsSelected;
        }

        public override bool IsDefaultSelected
        {
            get { return this.IsSelected == this.defaultIsSelected; }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetProperty(ref this.isSelected, value, nameof(IsSelected), nameof(IsDefaultSelected)); }
        }

        public override Task AddOrRemoveFeature()
        {
            if (this.IsSelected)
            {
                return this.AddFeature();
            }

            return this.RemoveFeature();
        }

        public override string ToString()
        {
            return string.Format($"{this.Title} - {this.IsSelected}");
        }

        protected virtual Task AddFeature()
        {
            return Task.FromResult<object>(null);
        }

        protected virtual Task RemoveFeature()
        {
            return Task.FromResult<object>(null);
        }
    }
}
