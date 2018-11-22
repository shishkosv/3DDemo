using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Oiler.Annotations;
using Oiler.Infrastructure.Constants;
using Oiler.Infrastructure.Utilities;
using Oiler.ViewModels;
using Oiler._3D.VisualModels;

namespace Oiler.Views
{
    /// <summary>
    /// Interaction logic for Scene.xaml
    /// </summary>
    public partial class Scene : UserControl, INotifyPropertyChanged
    {
        public Scene()
        {
            InitializeComponent();
            ViewModel = new SceneViewModel();
            this.MainModel.Children[0].GetName();
        }

        private SceneViewModel _viewModel;

        private SceneViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                this.DataContext = value;
            }
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var viewport = (HelixViewport3D)sender;
            var firstHit = viewport.Viewport.FindHits(e.GetPosition(viewport)).FirstOrDefault();
            if (firstHit != null)
            {
                ViewModel.SizeableViewModel.Select(firstHit.Visual);
            }
            else
            {
                ViewModel.SizeableViewModel.Select(null);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = ModelsBox.SelectedValue;
            UIElement3D controlToAdd = null;
            if (selected == null)
            {
                controlToAdd = new HouseVisual3D();
            }
            else
            {
                switch (selected)
                {
                    case Constants.HouseModelName:
                    {
                        controlToAdd = new HouseVisual3D();
                        controlToAdd.Transform = new TranslateTransform3D()
                        {
                            OffsetX = MathUtilities.GetRandomNumber(0, 120),
                            OffsetY = MathUtilities.GetRandomNumber(0, 120),
                            OffsetZ = 0
                        };
                    }
                        break;
                    case Constants.ChimneyModelName:
                    {
                        controlToAdd = new ChimneyVisual3D();
                        controlToAdd.Transform = new TranslateTransform3D()
                        {
                            OffsetX = MathUtilities.GetRandomNumber(0, 120),
                            OffsetY = MathUtilities.GetRandomNumber(0, 120),
                            OffsetZ = 0
                        };
                    }
                        break;
                    case Constants.WellModelName:
                    {
                        controlToAdd = new WellVisual3D();
                        controlToAdd.Transform = new TranslateTransform3D()
                        {
                            OffsetX = MathUtilities.GetRandomNumber(0, 120),
                            OffsetY = MathUtilities.GetRandomNumber(0, 120),
                            OffsetZ = -0
                        };
                    }
                        break;
                    default:
                    {
                        //create house by default
                        controlToAdd = new HouseVisual3D();
                        controlToAdd.Transform = new TranslateTransform3D()
                        {
                            OffsetX = MathUtilities.GetRandomNumber(0, 120),
                            OffsetY = MathUtilities.GetRandomNumber(0, 120),
                            OffsetZ = 0
                        };
                        }
                        break;
                }               
            }
                
            
            this.MainModel.Children.Add(controlToAdd);
            OnPropertyChanged("MainModel");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            Visual3D selected = (Visual3D)e.AddedItems[0];
            if (selected != null)
            {
                ViewModel.SizeableViewModel.Select(selected);
            }
            else
            {
                ViewModel.SizeableViewModel.Select(null);
            }
        }
    }
}
