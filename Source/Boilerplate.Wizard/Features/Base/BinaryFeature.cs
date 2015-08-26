namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Framework.ComponentModel;
    using Boilerplate.Wizard.Services;

    public abstract class BinaryFeature : NotifyPropertyChanges, IBinaryFeature
    {
        private readonly IProjectService projectService;
        private bool isSelected;

        public BinaryFeature(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public abstract string Description { get; }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetProperty(ref this.isSelected, value); }
        }

        public abstract string GroupName { get; }

        public abstract int Order { get; }

        public abstract string Title { get; }

        public Task AddOrRemoveFeature()
        {
            if (this.IsSelected)
            {
                return this.AddFeature();
            }

            return this.RemoveFeature();
        }

        protected IProjectService ProjectService
        {
            get { return this.projectService; }
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
