namespace Boilerplate.FeatureSelection.Features
{
    using System.Collections.Generic;

    public interface IMultiChoiceFeature : IFeature
    {
        bool IsMultiSelect { get; }

        FeatureItemCollection Items { get; }

        IEnumerable<IFeatureItem> ItemsView { get; }
    }
}
