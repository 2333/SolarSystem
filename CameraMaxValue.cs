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
                double max = 10000;
                max = camera.Position.X >= camera.Position.Y ? camera.Position.X : camera.Position.Y;
                max = max >= camera.Position.Z ? max : camera.Position.Z;
                max = max >= 10000?max:10000;
                return max*10;
            }else

            { return 10000; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
