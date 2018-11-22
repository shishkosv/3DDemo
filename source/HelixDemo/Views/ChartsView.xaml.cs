using System;
using System.Windows.Controls;

using Oiler.ViewModels;

namespace Oiler.Views
{
    /// <summary>
    /// Interaction logic for ChartsView.xaml
    /// </summary>
    public partial class ChartsView : UserControl
    {
        public ChartsView()
        {
            InitializeComponent();
            ViewModel = new ChartsViewModel();
        }

        private ChartsViewModel _viewModel;

        private ChartsViewModel ViewModel
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
            ViewModel.TickerSymbolChanged(e.AddedItems[0].ToString());
        }
    }
}
