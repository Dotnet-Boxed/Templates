namespace Boilerplate.Wizard.Views
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Documents;
    using Boilerplate.Wizard.ViewModels;

    public partial class MainView : Window, IMainView
    {
        public MainView()
        {
            InitializeComponent();
#if DEBUG
            this.Topmost = false;
#endif

            this.Loaded += MainView_Loaded;
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            this.OnOkClick(sender, e);
        }

        public IMainViewModel ViewModel
        {
            get { return (IMainViewModel)this.DataContext; }
            set { this.DataContext = value; }
        }

        private async void OnOkClick(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.Error == null)
            {
                await this.ViewModel.AddOrRemoveFeaturesCommand.Execute();
                if (this.ViewModel.Error == null)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            Process.Start(hyperlink.NavigateUri.ToString());
        }

        private void OnTitleMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
