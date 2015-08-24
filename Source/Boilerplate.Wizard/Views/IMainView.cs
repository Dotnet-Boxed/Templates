namespace Boilerplate.Wizard.Views
{
    using Boilerplate.Wizard.ViewModels;

    public interface IMainView
    {
        IMainViewModel ViewModel { get; set; }

        bool? ShowDialog();
    }
}
