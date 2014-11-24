// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolarSystem3D.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;
using HelixToolkit.Wpf;

namespace SolarsystemDemo
{
    public class SolarSystem3D : ModelVisual3D, INotifyPropertyChanged
    {
        public double DistanceScale { get; set; }
        public double DiameterScale { get; set; }

        private IList<BillboardTextItem> _textItem;
        public IList<BillboardTextItem> TextItem 
        {
            get { return _textItem; }
            set 
            { 
                _textItem = value;
                OnPropertyChanged("TextItem");
            }
        }
 
        double _days = 0;
        DateTime Time0;

        public double Days
        {
            get { return _days; }
            set { _days = value; OnPropertyChanged("Days"); OnPropertyChanged("Time"); TimeChanged(); }
        }

        public DateTime Time
        {
            get { return Time0.AddDays(_days); }
            set { Days = (value - Time0).TotalDays; }
        }

        private void TimeChanged()
        {
            UpdateModel(Time);
        }

        public SolarSystem3D()
        {
            Time0 = DateTime.Now;
            this._textItem = new List<BillboardTextItem>();
            this._textItem.Add(new BillboardTextItem { Text = "HXMT", Position = new Point3D(100, 100, 0), WorldDepthOffset = 50});
            this._textItem.Add(new BillboardTextItem { Text = "SUN", Position = new Point3D(0, 0, 0), WorldDepthOffset = 100 });
            //foreach(Satellite3D p in Children){
            //    if(p.ObjectName == "Moon")
            //        this._textItem.Add(new BillboardTextItem{Text = "Moon", Position = new Point3D(p.CalculatePosition)})

            //}
        }

        public void InitModel()
        {
            foreach (Planet3D p in Children)
                p.InitModel(this);
            //this.TextItem.Add(new BillboardTextItem{Text = "HXMT" ,Position = new Point3D(100,100,0), WorldDepthOffset= 50});
        }

        public void UpdateModel()
        {
            foreach (Planet3D p in Children)
                p.UpdateModel();
            //this.TextItem.Add(new BillboardTextItem { Text = "HXMT", Position = new Point3D(100, 100, 0), WorldDepthOffset = 50});
            //OnPropertyChanged("TextItem");
        }
        public void UpdateModel(DateTime time)
        {
            foreach (Planet3D p in Children)
                p.UpdateModel(time);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}