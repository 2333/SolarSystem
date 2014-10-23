// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Planet3D.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;
using System.Windows;
using System.IO;
using System.Collections;

namespace SolarsystemDemo
{
    public class Planet3D : TexturedObject3D
    {
        public double Aphelion { get; set; }
        public double Perifelion { get; set; }
        public double SemiMajorAxis { get; set; }
        public double Eccentricity { get; set; }
        public double OrbitalPeriod { get; set; }

        /// <summary>
        /// Inclination to invariable plane
        /// http://en.wikipedia.org/wiki/Inclination
        /// </summary>
        public double Inclination { get; set; }
        public double LongitudeOfAscendingNode { get; set; }
        public double ArgumentOfPerihelion { get; set; }

        /// <summary>
        /// http://en.wikipedia.org/wiki/Axial_tilt`
        /// </summary>
        public double AxialTilt { get; set; }

        /// <summary>
        /// http://en.wikipedia.org/wiki/Rotation_period
        /// </summary>
        public double RotationPeriod { get; set; }

        public SolarSystem3D SolarSystem { get; set; }
        public List<Satellite3D> Satellites { get; set; }
        public double DistanceScale { get; set; }
        public double DiameterScale { get; set; }

        /// <summary>
        /// For BillboardTextVisual3D
        /// </summary>
        public Brush foreground { get; set; }
        public Brush background { get; set; }
        public Brush borderbrush { get; set; }
        public Thickness borderthickness { get; set; }
        public double height { get; set; }
        public Thickness padding { get; set; }
        public double depthoffset { get; set; }
        public FontFamily font { get; set; }

        private Point3D SignPosition;
        

        BillboardTextVisual3D Sign;

        TubeVisual3D orbit;
        public Point3D Position { get; set; }
        
        public Planet3D()
        {
            Satellites = new List<Satellite3D>();

            orbit = new TubeVisual3D() {Diameter=0.8, ThetaDiv = 16 };
            orbit.Material = MaterialHelper.CreateMaterial(null,Brushes.Blue,Brushes.Gray,0.5, 20);
            Children.Add(orbit);
            Sign = new BillboardTextVisual3D()
            {
                Height = height,
                Padding = padding,
                FontSize = 25,               
            };
            Children.Add(Sign);
        }

        public Point3D CalculatePosition(double angle, double scale)
        {
            double a = SemiMajorAxis / scale;
            double e = Eccentricity;
            double b = a * Math.Sqrt(1 - e * e);
            Point3D p = new Point3D(Math.Cos(angle) * a, Math.Sin(angle) * b, 0);
            return p;
        }
        public Point3DCollection ReadPosition(string fileName)
        {
            StreamReader fileReader = new StreamReader("../../Orbit/" + fileName + ".txt");
            string newLine = "";
            ArrayList posList = new ArrayList();

            while(newLine!= null)
            {
                newLine = fileReader.ReadLine();
                if (newLine != null)
                {
                    newLine = newLine.Trim();
                    string[] tmp = newLine.Split(' ');
                    for (int i = 0; i <= 2; i++)
                        posList.Add(tmp[i]);
                }
            }
                    
            fileReader.Close();
            Point3D p;
            Point3DCollection pointCollection = new Point3DCollection();
            for (int i = 0; i < posList.Count; )
            {
                p = new Point3D(Convert.ToDouble(posList[i++]) / SolarSystem.DistanceScale,
                        Convert.ToDouble(posList[i++]) / SolarSystem.DistanceScale,
                        Convert.ToDouble(posList[i++]) / SolarSystem.DistanceScale);
                    pointCollection.Add(p);
            }
                return pointCollection;
        }

        public void UpdateTransform()
        {
            var tg = new Transform3DGroup();
            // http://en.wikipedia.org/wiki/Argument_of_periapsis
            //  tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0,0,1),LongitudeOfAscendingNode)));
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Inclination)));
            //  tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1,0,0),ArgumentOfPerihelion)));
            Transform = tg;
        }

        public void UpdateOrbit()
        {
            try
            {
                if (ObjectName == "Venus" || ObjectName == "Mercury" || ObjectName == "Earth" || ObjectName == "Mars") 
                {
                orbit.Path = ReadPosition(ObjectName);
                orbit.UpdateModel();
                }
                else if (SemiMajorAxis > 0)
                {
                    int n = 90;
                    var path = new Point3DCollection();
                    for (int i = 0; i < n; i++)
                        path.Add(CalculatePosition((double)i / (n - 1) * Math.PI * 2, SolarSystem.DistanceScale));

                    orbit.Path = path;
                    orbit.UpdateModel();
                }
            }
            catch (Exception)
            {

                throw;
            }

            Sphere.Radius = MeanRadius / SolarSystem.DiameterScale;
        }

        void UpdatePosition()
        {
            double ang = 0;
            if (OrbitalPeriod > 0)
                ang = SolarSystem.Days / OrbitalPeriod * Math.PI * 2;
            Position = CalculatePosition(ang, SolarSystem.DistanceScale);
            SignPosition = new Point3D(Position.X, Position.Y, Position.Z);
            // http://en.wikipedia.org/wiki/Axial_tilt
            // http://en.wikipedia.org/wiki/Rotation_period
            var rotang = SolarSystem.Days / RotationPeriod * 360;
            var axis = new Vector3D(0, 0, 1); // todo: should be normal to orbital plane

            var tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(axis, rotang)));
            tg.Children.Add(new TranslateTransform3D(Position.X, Position.Y, Position.Z));
            Sphere.Transform = tg;
            var stg = new Transform3DGroup();
            stg.Children.Add(new TranslateTransform3D(Position.X, Position.Y-1e1, Position.Z+1e1));
            Sign.Foreground = foreground;
            Sign.Background = background;
            Sign.BorderThickness = borderthickness;
            Sign.FontFamily = font;
            //Sign.Position = Position;
            Sign.Text = base.ObjectName;
            Sign.Transform = stg;
        }

        public void InitModel(SolarSystem3D ss)
        {
            SolarSystem = ss;
            UpdateTransform();
            UpdateOrbit();
            foreach (var s in Satellites)
            {
                s.InitModel(this);
                Children.Add(s);
            }
        }

        public void UpdateModel()
        {
            UpdatePosition();
            foreach (var s in Satellites)
                s.UpdateModel();
        }
        
    }
}