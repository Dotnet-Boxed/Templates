namespace Boilerplate.FeatureSelection.Views
{
    using Boilerplate.FeatureSelection.ViewModels;

    public interface IMainView
    {
        IMainViewModel ViewModel { get; set; }

        bool? ShowDialog();
    }
}
