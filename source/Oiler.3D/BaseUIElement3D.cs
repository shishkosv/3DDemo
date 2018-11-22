using System.Windows;
using HelixToolkit.Wpf;
using PropertyTools.DataAnnotations;

namespace Oiler._3D
{
    public class BaseUiElement3D : UIElement3D
    {
        public static readonly DependencyProperty NameProperty =
            DependencyPropertyEx.Register<string, BaseUiElement3D>("Name", "Visual Name");

        [Category("Base Properties")]
        [System.ComponentModel.Browsable(true)]
        public string Name
        {
            get => (string) GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
    }
}