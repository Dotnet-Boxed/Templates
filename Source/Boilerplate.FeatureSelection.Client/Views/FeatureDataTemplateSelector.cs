namespace Boilerplate.FeatureSelection.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using Boilerplate.FeatureSelection.Features;

    public class FeatureDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BinaryChoiceFeatureDataTemplate { get; set; }

        public DataTemplate MultiChoiceFeatureDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            IBinaryChoiceFeature binaryChoiceFeature = item as IBinaryChoiceFeature;
            if (binaryChoiceFeature != null)
            {
                return this.BinaryChoiceFeatureDataTemplate;
            }

            IMultiChoiceFeature multiChoiceFeature = item as IMultiChoiceFeature;
            if (multiChoiceFeature != null)
            {
                return this.MultiChoiceFeatureDataTemplate;
            }

            return null;
        }
    }
}
