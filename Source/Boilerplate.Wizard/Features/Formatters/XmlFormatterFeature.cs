namespace Boilerplate.Wizard.Features
{
    using System;
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class XmlFormatterFeature : BinaryFeature
    {
        public XmlFormatterFeature(IProjectService projectService)
            : base(projectService)
        {
            this.IsSelected = true;
        }

        public override string Title
        {
            get { return "XML Formatter"; }
        }

        public override string Description
        {
            get { return "Choose whether to use an XML formatter to input and output XML."; }
        }

        public override string GroupName
        {
            get { return "Formatters"; }
        }

        public override int Order
        {
            get { return 3; }
        }

        protected override Task RemoveFeature()
        {
            if (this.IsSelected)
            {
                return this.ProjectService.DeleteComment(
                    "XmlFormatter",
                    DeleteCommentMode.StartEndComment);
            }

            return this.ProjectService.DeleteComment(
                "XmlFormatter",
                DeleteCommentMode.StartEndCommentAndCode);
        }
    }
}
