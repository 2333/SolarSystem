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
using System.Runtime.InteropServices;

namespace SolarsystemDemo
{
    public class Planet3D : TexturedObject3D
    {
        [DllImport("LibExport.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void fnLibExport(int y, int m, int d, int h, int mi, double sec,
    int a, int aT, int b, int bT, double[] mat, double[] dis, string path, int pathlen);

        public IList<BillboardTextItem> TextItem;
        public double Aphelion { get; set; }
        public double Perifelion { get; set; }
        public double SemiMajorAxis { get; set; }
        public double Eccentricity { get; set; }
        public double OrbitalPeriod { get; set; }
        //public bool Ischecked { get; set; }
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
        //SolarSystem3D ss = new SolarSystem3D();
        

        BillboardTextVisual3D Sign;

        TubeVisual3D orbit;
        public Point3D Position { get; set; }
        DateTime time0;
        double[] mat = new double[9];//行星的变换矩阵
        double[] dis = new double[3];//行星与太阳距离
        public Planet3D()
        {
            Satellites = new List<Satellite3D>();

            orbit = new TubeVisual3D() {Diameter=0.8, ThetaDiv = 160 };
            orbit.Material = MaterialHelper.CreateMaterial(null,Brushes.Blue,Brushes.Gray,0.5, 0);
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
        //使用dll计算行星位置
        public Point3D CalculatePosition(double scale, DateTime time, int planetnum)
        {
            
            //DateTime time = DateTime.Now;
            int year = time.Year;
            int month = time.Month;
            int day = time.Day;
            int hour = time.Hour;
            int min = time.Minute;
            double sec = time.Second;
            int a = 4;//原点坐标系
            int aT = 11;//原点行星代码，11为太阳
            int b = 1;//目标坐标系
            int bT = planetnum;//目标行星代码，1-8从近到远
            string path = "C:\\spicedata";
            int pathlen = path.Length;
            //fnLibExport(year,month,day,hour,min,sec,a,aT,b,bT,mat,dis,path,pathlen);
             try 
	        {	        
		        ZBZH_PLANET(year, month, day, hour, min, sec, a, aT, b, bT, mat, dis, path, pathlen);
	        }
	        catch (Exception ex)
	        {
		
		        throw ex;
	        }

            Point3D p = new Point3D(dis[0]/scale,dis[1]/scale,dis[2]/scale);
            return p;
        }
        //从文件中读取行星的轨道数据
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

            if (base.ObjectName != "Sun")
                stg.Children.Add(new TranslateTransform3D(Position.X, Position.Y - 1e1, Position.Z + Sphere.Radius * 2));
            else
            {
                stg.Children.Add(new TranslateTransform3D(0, 0, 0));
                Sign.Height = 0;
            }
            Sign.Foreground = foreground;
            Sign.Background = background;
            Sign.BorderThickness = borderthickness;
            Sign.FontFamily = font;
            Sign.Text = base.ObjectName;
            Sign.Transform = stg;
        }

        void UpdatePosition(DateTime time)
        {
            double ang = 0;
            if (OrbitalPeriod > 0)
                ang = SolarSystem.Days / OrbitalPeriod * Math.PI * 2;
            Position = CalculatePosition(SolarSystem.DistanceScale, time, Convert.ToInt32(Enum.Parse(typeof(planetnum),base.ObjectName)));
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

            if (base.ObjectName != "Sun")
                stg.Children.Add(new TranslateTransform3D(Position.X, Position.Y - 1e1, Position.Z + Sphere.Radius * 2));
            else
            {
                stg.Children.Add(new TranslateTransform3D(0, 0, 0));
                Sign.Height = 0;
            }
            Sign.Foreground = foreground;
            Sign.Background = background;
            Sign.BorderThickness = borderthickness;
            Sign.FontFamily = font;
            Sign.Text = base.ObjectName;
            Sign.Transform = stg;
        }

        public void InitModel(SolarSystem3D ss)
        {
            SolarSystem = ss;
            time0 = ss.Time;
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
            if (base.ObjectName != "Sun")
                UpdatePosition(time0);
            else
                UpdatePosition();
            foreach (var s in Satellites)
                s.UpdateModel();
        }
        public void UpdateModel(DateTime time)
        {
            if (base.ObjectName != "Sun")
                UpdatePosition(time);
            else
                UpdatePosition();
            foreach (var s in Satellites)
                s.UpdateModel();
        }
        internal enum planetnum : int
        {
            Mercury = 1,
            Venus = 2,
            Earth = 3,
            Mars = 4,
            Jupiter = 5,
            Saturn = 6,
            Uranus = 7,
            Neptune = 8
        }
     
        [DllImport("ZBZH_PLANET.dll")]
        static extern void ZBZH_PLANET(int y, int m, int d, int h, int mi, double sec,
    int a, int aT, int b, int bT, double[] mat, double[] dis, string path, int pathlen);
    }
}