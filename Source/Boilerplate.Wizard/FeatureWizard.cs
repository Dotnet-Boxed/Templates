namespace Boilerplate.Wizard
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TemplateWizard;

    public class FeatureWizard : IWizard
    {
        #region Public Methods

        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(global::EnvDTE.Project project)
        {
            string projectFilePath = project.FileName;
            string solutionFilePath = project.CodeModel.DTE.Solution.FileName;
            new Startup().Execute(projectFilePath);
        }

        public void ProjectItemFinishedGenerating(global::EnvDTE.ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(
            object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams)
        {
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        #endregion
    }
}
