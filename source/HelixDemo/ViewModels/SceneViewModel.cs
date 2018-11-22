using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using Oiler.Infrastructure.Constants;
using Oiler._3D.VisualViewModels;

namespace Oiler.ViewModels
{
    public class SceneViewModel
    {
        public SurfacePlotViewModel SurfacePlotViewModel { get; set; }

        public WellViewModel WellViewModel { get; set; }

        public SizeableViewModel SizeableViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public SceneViewModel()
        {
            SurfacePlotViewModel = new SurfacePlotViewModel();
            WellViewModel = new WellViewModel();
            SizeableViewModel = new SizeableViewModel();
            VisualModels = new ObservableCollection<string>(Constants.VisualModels);
        }

        public Model3D Model { get; set; }

        public ObservableCollection<string> VisualModels { get; set; }
    }
}
