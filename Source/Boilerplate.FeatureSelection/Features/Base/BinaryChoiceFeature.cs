namespace Boilerplate.FeatureSelection.Features
{
    using System;
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public abstract class BinaryChoiceFeature : Feature, IBinaryChoiceFeature
    {
        private bool isSelected;

        public BinaryChoiceFeature(IProjectService projectService)
            : base(projectService)
        {
            this.isSelected = this.IsSelectedDefault;
        }

        public abstract bool IsSelectedDefault { get; }

        public virtual string Icon { get { return null; } }

        public override bool IsDefaultSelected
        {
            get { return this.IsSelected == this.IsSelectedDefault; }
        }

        public virtual bool IsSelected
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
            return string.Format($"{this.Title} Selected:<{this.IsSelected}> DefaultIsSelected:<{this.IsDefaultSelected}>");
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
