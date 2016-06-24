namespace Boilerplate.Wizard
{
    using System.Collections.Generic;
    using Boilerplate.FeatureSelection;
    using Boilerplate.FeatureSelection.Features;
    using Microsoft.VisualStudio.TemplateWizard;

    public class Mvc6ApiFeatureWizard : IWizard
    {
        public void BeforeOpeningFile(global::EnvDTE.ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(global::EnvDTE.Project project)
        {
            string projectFilePath = project.FileName;
            new Startup().Execute(projectFilePath, FeatureSet.Mvc6Api);
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
    }
}
