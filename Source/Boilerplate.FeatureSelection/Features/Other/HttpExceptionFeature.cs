namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class HttpExceptionFeature : BinaryChoiceFeature
    {
        public HttpExceptionFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Allows the use of HttpException as an alternative method of returning an error result. This feature existed in MVC 5 but was dropped for MVC 6, here it is back again if you need it."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Other; }
        }

        public override string Id
        {
            get { return "HttpException"; }
        }

        public override bool IsSelectedDefault
        {
            get { return false; }
        }

        public override bool IsVisible
        {
            get { return true; }
        }

        public override int Order
        {
            get { return 5; }
        }

        public override string Title
        {
            get { return "HttpException"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ApplicationBuilderExtensions.cs");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ApplicationBuilderExtensions.cs");
        }
    }
}