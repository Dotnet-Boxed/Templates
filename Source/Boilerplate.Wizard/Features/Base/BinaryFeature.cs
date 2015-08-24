namespace Boilerplate.Wizard.Features
{
    using Framework.ComponentModel;
    using Services;

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

        public void AddOrRemoveFeature()
        {
            if (this.IsSelected)
            {
                this.AddFeature();
            }
            else
            {
                this.RemoveFeature();
            }
        }

        protected IProjectService ProjectService
        {
            get { return this.projectService; }
        }

        protected virtual void AddFeature()
        {
        }

        protected virtual void RemoveFeature()
        {
        }
    }
}
