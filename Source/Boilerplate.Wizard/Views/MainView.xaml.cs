namespace Boilerplate.Wizard.Views
{
    using System.Windows;
    using Boilerplate.Wizard.ViewModels;

    public partial class MainView : Window, IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }

        public IMainViewModel ViewModel
        {
            get { return (IMainViewModel)this.DataContext; }
            set { this.DataContext = value; }
        }

        private void OnAddRemoveFeaturesClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.AddOrRemoveFeaturesCommand.Execute();
            this.Close();
        }
    }
}
