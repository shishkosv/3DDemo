using System.Windows;
using System.Windows.Controls;
using Oiler.Infrastructure.Constants;
using Oiler.ViewModels;

namespace Oiler
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Using block is here to make sure we release native memory right away
            //using (var wrapper = new Oiler.Algo.CLI.WeatherService())
            //{
            //    MessageBox.Show("The answer is " + wrapper.Get());
            //}
        }

        private MainViewModel _viewModel;

        private MainViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                this.DataContext = value;
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newMode = e.AddedItems[0].ToString();
            ViewModel.ModeChanged(newMode);

            if (newMode == Constants.ViewModes[1])
            {
                this.ChartsView.Visibility = Visibility.Collapsed;
                this.SceneView.Visibility = Visibility.Visible;
            }

            else
            {
                this.SceneView.Visibility = Visibility.Collapsed;
                this.ChartsView.Visibility = Visibility.Visible;
            }
        }
    }
}