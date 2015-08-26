namespace Boilerplate.Wizard.Features
{
    using System;
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class BsonFormatterFeature : BinaryFeature
    {
        public BsonFormatterFeature(IProjectService projectService)
            : base(projectService)
        {
            this.IsSelected = true;
        }

        public override string Title
        {
            get { return "BSON Formatter"; }
        }

        public override string Description
        {
            get { return "Choose whether to use a BSON (Binary JSON) formatter to input and output BSON."; }
        }

        public override string GroupName
        {
            get { return "Formatters"; }
        }

        public override int Order
        {
            get { return 4; }
        }

        protected override Task RemoveFeature()
        {
            if (this.IsSelected)
            {
                return this.ProjectService.DeleteComment(
                    "BsonFormatter",
                    DeleteCommentMode.StartEndComment);
            }

            return this.ProjectService.DeleteComment(
                "BsonFormatter",
                DeleteCommentMode.StartEndCommentAndCode);
        }
    }
}
