
using System.ComponentModel;
using System.Windows.Media.Media3D;
using PropertyTools;

namespace Oiler._3D.VisualViewModels
{
    public class SizeableViewModel : Observable
    {
        private object selectedObject;

        public object SelectedObject
        {
            get
            {
                return this.selectedObject;
            }

            set
            {
                this.SetValue(ref this.selectedObject, value, () => this.SelectedObject);
            }
        }

        public void Select(Visual3D visual)
        {
            this.SelectedObject = visual;
        }

        private double translateValue;
        public double TranslateValue
        {
            get { return translateValue; }
            set
            {
                if (translateValue != value)
                {
                    translateValue = value;
                    RaisePropertyChanged("TranslateValue");
                }
            }
        }

        private double rotateValue;
        public double RotateValue
        {
            get { return rotateValue; }
            set
            {
                if (rotateValue != value)
                {
                    rotateValue = value;
                    RaisePropertyChanged("RotateValue");
                }
            }
        }
    }
}