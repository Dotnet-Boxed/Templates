namespace Boilerplate.Wizard
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TemplateWizard;

    public class Mvc6FeatureWizard : IWizard
    {
        #region Public Methods

        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(global::EnvDTE.Project project)
        {
            string projectFilePath = project.FileName;
            new Startup().Execute(projectFilePath, FeatureSet.Mvc6);
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
