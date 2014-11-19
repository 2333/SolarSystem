using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace SolarsystemDemo
{
    public class CameraMaxValue:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value!=null)
            {
                ProjectionCamera camera = value as ProjectionCamera;
                double max = 1000000;
                double xPosition = Math.Abs(camera.Position.X);
                double yPosition = Math.Abs(camera.Position.Y);
                double zPosition = Math.Abs(camera.Position.Z);
                max = xPosition >= yPosition ? xPosition : yPosition;
                max = max >= zPosition ? max : zPosition;
                max = max >= 10000?max:10000;
                return max*10;
            }else

            { return 1000000; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
