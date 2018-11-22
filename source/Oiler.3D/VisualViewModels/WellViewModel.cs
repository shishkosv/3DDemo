using System.Linq;
using System.Windows.Media;
using HelixToolkit.Wpf;
using Oiler.Infrastructure;
using Oiler._3D._3DUtiliites;

namespace Oiler._3D.VisualViewModels
{
    public class WellViewModel : BaseViewModel
    {
        public WellViewModel()
        {
            ColorCoding = ColorCoding.ByGradientY;
            UpdateModel();
        }

        public double[,] ColorValues { get; set; }

        public Brush SurfaceBrush
        {
            get
            {
                switch (ColorCoding)
                {
                    case ColorCoding.ByGradientY:
                        return BrushHelper.CreateGradientBrush(Colors.Red, Colors.White, Colors.Blue);
                    case ColorCoding.ByLights:
                        return Brushes.White;
                }
                return null;
            }
        }

        public ColorCoding ColorCoding { get; set; }

        private void UpdateModel()
        {
            ColorValues = new double[1,1];
            ColorValues[0, 0] = 134;
            RaisePropertyChanged("ColorValues");
            RaisePropertyChanged("SurfaceBrush");
        }
    }
}