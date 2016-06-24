namespace Boilerplate.FeatureSelection.Views
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Documents;
    using Boilerplate.FeatureSelection.ViewModels;

    public partial class MainView : Window, IMainView
    {
        public MainView()
        {
            InitializeComponent();
#if DEBUG
            this.Topmost = false;
#endif
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
