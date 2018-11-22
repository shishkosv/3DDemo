
using System.Collections.ObjectModel;
using Oiler.Infrastructure.Constants;
using Prism.Mvvm;

namespace Oiler.ViewModels
{
    public class MainViewModel : BindableBase
    {

        private string _currentMode;

        private ObservableCollection<string> _modes;


        public MainViewModel()
        {
        }

        public void ModeChanged(string newMode)
        {
            this.CurrentMode = newMode;
        }

        public string CurrentMode
        {
            get
            {
                return _currentMode;
            }
            set
            {
                SetProperty(ref _currentMode, value);
            }
        }

        public ObservableCollection<string> ViewModesCollection
        {
            get
            {
                return new ObservableCollection<string>(Constants.ViewModes);
            }
            private set
            {
                SetProperty(ref _modes, value);
            }
        }
    }
}
