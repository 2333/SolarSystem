// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Window1.xaml.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace SolarsystemDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        SolarSystem3D SolarSystem;

        public Window1()
        {
            InitializeComponent();

            view1.Camera.Position = new Point3D(0, 400, 500);
            view1.Camera.LookDirection = new Vector3D(0, -400, -500);
            //view1.Camera.FarPlaneDistance = 10000;
            //view1.CameraController.
            SolarSystem = view1.Children[2] as SolarSystem3D;
            
            Loaded += new RoutedEventHandler(Window1_Loaded);
            DataContext = SolarSystem;
            
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            SolarSystem.InitModel();
            SolarSystem.UpdateModel();            
        }

        private void EarthView_Checked(object sender, RoutedEventArgs e)
        {
            if(view1!=null)
            {
                foreach (Planet3D p in SolarSystem.Children)
                    if (p.ObjectName == "Earth")
                    {
                        view1.Camera.Position = new Point3D(p.Position.X + 50, p.Position.Y + 50, p.Position.Z + 50);
                        view1.Camera.LookDirection = new Vector3D(-50,-50,-50);
                        view1.Camera.UpDirection = new Vector3D(0, 0, 1);
                    }

            }
        }

        private void EarthView_Unchecked(object sender, RoutedEventArgs e)
        {
            view1.Camera.Position = new Point3D(0, 400, 500);
            view1.Camera.LookDirection = new Vector3D(0, -400, -500);
        }
    }
}