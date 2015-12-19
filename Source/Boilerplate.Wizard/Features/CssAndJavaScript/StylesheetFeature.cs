namespace Boilerplate.Wizard.Features
{
    using System;
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class StylesheetFeature : MultiChoiceFeature
    {
        private readonly IFeatureItem sass;
        private readonly IFeatureItem less;

        public StylesheetFeature(IProjectService projectService)
            : base(projectService)
        {
            this.sass = new FeatureItem(
                "Sass",
                "SASS",
                "Sass (Syntactically Awesome Stylesheets) is a style sheet language initially designed by Hampton Catlin and developed by Natalie Weizenbaum.",
                1,
                "/Boilerplate.Wizard;component/Assets/sass_logo.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.sass);

            this.less = new FeatureItem(
                "Less",
                "LESS",
                "Designed by Alexis Sellier, Less is influenced by Sass and has influenced the newer \"SCSS\" syntax of Sass, which adapted its CSS-like block formatting syntax",
                2,
                "/Boilerplate.Wizard;component/Assets/LESS_Logo.png",
                false);
            this.Items.Add(this.less);
        }

        public override string Description
        {
            get { return "The type of dynamic style sheet language that can be compiled into Cascading Style Sheets (CSS)."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.CssAndJavaScript; }
        }

        public override string Id
        {
            get { return "StylesheetLanguage"; }
        }

        public override bool IsVisible
        {
            get { return true; }
        }

        public override int Order
        {
            get { return 6; }
        }

        public override string Title
        {
            get { return "Stylesheet Language"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.less.IsSelected)
            {
                await this.ProjectService.DeleteFile("Styles/site.scss");

                await this.ProjectService.EditCommentInFile(this.sass.CommentName, EditCommentMode.DeleteCode, "package.json");
                await this.ProjectService.EditCommentInFile(this.sass.CommentName, EditCommentMode.DeleteCode, "gulpfile.js");
            }
            else
            {
                await this.ProjectService.DeleteFile("Styles/site.less");

                await this.ProjectService.EditCommentInFile(this.less.CommentName, EditCommentMode.DeleteCode, "package.json");
                await this.ProjectService.EditCommentInFile(this.less.CommentName, EditCommentMode.DeleteCode, "gulpfile.js");
            }
        }
    }
}
