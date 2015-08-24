namespace Boilerplate.Wizard.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using Boilerplate.Wizard.Features;

    public class FeatureDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BinaryFeatureDataTemplate { get; set; }

        public DataTemplate MultiChoiceFeatureDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            IBinaryFeature binaryFeature = item as IBinaryFeature;
            if (binaryFeature != null)
            {
                return this.BinaryFeatureDataTemplate;
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
