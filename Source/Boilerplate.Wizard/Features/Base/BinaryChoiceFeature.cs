namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public abstract class BinaryChoiceFeature : Feature, IBinaryChoiceFeature
    {
        private bool isSelected;

        public BinaryChoiceFeature(IProjectService projectService)
            : base(projectService)
        {
            this.isSelected = this.IsDefaultSelected;
        }

        public virtual string Icon { get { return null; } }

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
