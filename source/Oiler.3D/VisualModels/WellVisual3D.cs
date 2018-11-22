using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using PropertyTools.DataAnnotations;
using BrowsableAttribute = System.ComponentModel.BrowsableAttribute;

namespace Oiler._3D.VisualModels
{
    public class WellVisual3D : BaseUiElement3D
    {
        public static readonly DependencyProperty HeightProperty =
            DependencyPropertyEx.Register<double, WellVisual3D>("Height", 80, (s, e) => s.AppearanceChanged());

        public static readonly DependencyProperty BaseDiameterProperty =
            DependencyPropertyEx.Register<double, WellVisual3D>("BaseDiameter", 3, (s, e) => s.AppearanceChanged());

        public static readonly DependencyProperty PositionProperty =
            DependencyPropertyEx.Register<Point3D, WellVisual3D>("Position", new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged());
        public static readonly DependencyProperty TopPositionProperty =
            DependencyPropertyEx.Register<Point3D, WellVisual3D>("TopPosition", new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged());

        private GeometryModel3D _horizontal = new GeometryModel3D();
        private GeometryModel3D _vertical = new GeometryModel3D();

        public WellVisual3D()
        {
            _horizontal.Material = MaterialHelper.CreateMaterial(Brushes.Brown, ambient: 10);
            _vertical.Material = MaterialHelper.CreateMaterial(Brushes.BlueViolet, ambient: 10);
            this.AppearanceChanged();
            var model = new Model3DGroup();
            model.Children.Add(_horizontal);
            model.Children.Add(_vertical);
            this.Visual3DModel = model;
        }

        public Point3D Position
        {
            get { return (Point3D)this.GetValue(PositionProperty); }
            set { this.SetValue(PositionProperty, value); }
        }

        public Point3D TopPosition
        {
            get { return (Point3D)this.GetValue(TopPositionProperty); }
            set { this.SetValue(TopPositionProperty, value); }
        }

        [Slidable(0, 100)]
        [FormatString("0.00")]
        [Browsable(true)]
        public double Height
        {
            get { return (double)this.GetValue(HeightProperty); }
            set { this.SetValue(HeightProperty, value); }
        }

        [Category("Well Properties")]
        [Slidable(0, 20)]
        [FormatString("0.00")]
        [Browsable(true)]
        public double BaseDiameter
        {
            get { return (double)this.GetValue(BaseDiameterProperty); }
            set { this.SetValue(BaseDiameterProperty, value); }
        }

        private void AppearanceChanged()
        {
            var redbuilder = new MeshBuilder(false, false);
            var whitebuilder = new MeshBuilder(false, false);
            for (int i = 0; i < 1; i++)
            {
                var f0 = (double)i;
                var f1 = (double)(i + 1);
                var y0 = this.Height * f0;
                var y1 = this.Height * f1;
                var d0 = (this.BaseDiameter * (1 - f0)) + (this.BaseDiameter * f0);
                var d1 = (this.BaseDiameter * (1 - f1)) + (this.BaseDiameter * f1);
                var builder = (1 - i) % 2 == 1 ? redbuilder : whitebuilder;
                builder.AddCone(this.Position + new Vector3D(0, 0, y0),
                    new Vector3D(0, 0, -1), d0 / 2, d1 / 2, y1 - y0, i == 0, i == 0, 20);
            }

            this.TopPosition = this.Position - new Vector3D(0, 0, this.Height);

            _vertical.Geometry = redbuilder.ToMesh(true);
            _horizontal.Geometry = whitebuilder.ToMesh(true);
        }
    }
}