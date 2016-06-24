namespace Boilerplate.FeatureSelection.Features
{
    using System;
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class AssemblyCopyrightFeature : BinaryChoiceFeature
    {
        public AssemblyCopyrightFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Sets the AssemblyCopyright in the AssemblyInfo.cs file to the current user and year."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Hidden; }
        }

        public override string Id
        {
            get { return "AssemblyCopyright"; }
        }

        public override bool IsSelectedDefault
        {
            get { return true; }
        }

        public override bool IsVisible
        {
            get { return false; }
        }

        public override int Order
        {
            get { return 4; }
        }

        public override string Title
        {
            get { return "Set the AssemblyCopyright"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.ReplaceInFile(
                "[assembly: AssemblyCopyright(\"\")]",
                $"[assembly: AssemblyCopyright(\"Copyright © {Environment.UserName} {DateTime.UtcNow.Year}\")]",
                @"Properties\AssemblyInfo.cs");
        }
    }
}
