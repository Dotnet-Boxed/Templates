namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;
    using Framework.ComponentModel;

    public class TargetFrameworkFeature : MultiChoiceFeature
    {
        private IFeatureItem net461;
        private IFeatureItem netCore;

        public TargetFrameworkFeature(IProjectService projectService)
            : base(projectService)
        {
            this.netCore = new FeatureItem(
                "NetCore",
                ".NET Core",
                "Target the .NET Core framework.",
                1,
                "/Boilerplate.FeatureSelection;component/Assets/NetCore.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.netCore);

            this.net461 = new FeatureItem(
                "NetFramework",
                ".NET Framework 4.6.1",
                "Target the full .NET Framework 4.6.1.",
                2,
                "/Boilerplate.FeatureSelection;component/Assets/NetFramework.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.net461);
        }

        public override string Description
        {
            get { return "Decide which version of the .NET Framework to target. .NET Core can run cross platform (on Mac and Linux) and the .NET Core framework and runtime can be shipped with the application so it is fully stand-alone while the .NET Framework 4.6.1 gives you access to the full breadth of libraries available in .NET instead of the subset available in .NET Core."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Project; }
        }

        public override string Id
        {
            get { return "TargetFramework"; }
        }

        public override bool IsDefaultSelected
        {
            get { return this.net461.IsSelected && this.netCore.IsSelected; }
        }

        public override bool IsMultiSelect { get; } = true;

        public override bool IsVisible
        {
            get { return true; }
        }

        public override int Order
        {
            get { return 1; }
        }

        public override string Title
        {
            get { return "Target Framework"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.net461.IsSelected && this.netCore.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    $"{this.Id}-Both",
                    EditCommentMode.LeaveCodeUnchanged,
                    this.ProjectService.ProjectFileName);
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.DeleteCode,
                    this.ProjectService.ProjectFileName);
                await this.ProjectService.EditCommentInFile(
                    this.netCore.CommentName,
                    EditCommentMode.DeleteCode,
                    this.ProjectService.ProjectFileName);
            }
            else if (this.net461.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    $"{this.Id}-Both",
                    EditCommentMode.DeleteCode,
                    this.ProjectService.ProjectFileName);
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.UncommentCode,
                    this.ProjectService.ProjectFileName);
                await this.ProjectService.EditCommentInFile(
                    this.netCore.CommentName,
                    EditCommentMode.DeleteCode,
                    this.ProjectService.ProjectFileName);
            }
            else if (this.netCore.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    $"{this.Id}-Both",
                    EditCommentMode.DeleteCode,
                    this.ProjectService.ProjectFileName);
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.DeleteCode,
                    this.ProjectService.ProjectFileName);
                await this.ProjectService.EditCommentInFile(
                    this.netCore.CommentName,
                    EditCommentMode.UncommentCode,
                    this.ProjectService.ProjectFileName);
            }

            if (this.net461.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    "ReadMe.html");
            }
            else
            {
                this.ProjectService.DeleteFile("app.config");
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.DeleteCode,
                    "ReadMe.html");
            }

            if (this.net461.IsSelected && !this.netCore.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.DeleteCode,
                    @"Controllers\HomeController.cs");
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.DeleteCode,
                    @"Services\Feed\FeedService.cs");
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.DeleteCode,
                    @"Services\Feed\IFeedService.cs");
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.DeleteCode,
                    @"ServiceCollectionExtensions.cs");
            }
            else
            {
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    @"Controllers\HomeController.cs");
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    @"Services\Feed\FeedService.cs");
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    @"Services\Feed\IFeedService.cs");
                await this.ProjectService.EditCommentInFile(
                    this.net461.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    @"ServiceCollectionExtensions.cs");
            }
        }

        protected override void OnItemChanged(object sender, ItemChangedEventArgs<IFeatureItem> e)
        {
            base.OnItemChanged(sender, e);

            if (!this.net461.IsSelected && !this.netCore.IsSelected)
            {
                if (e.Item == this.net461)
                {
                    this.netCore.IsSelected = true;
                }
                else
                {
                    this.net461.IsSelected = true;
                }
            }
        }
    }
}