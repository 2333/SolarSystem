using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SolarsystemDemo
{
    public class PlanetData
    {
        [DllImport("ZBZH_PLANET.dll",CallingConvention=CallingConvention.Cdecl)]
        public static extern void fnLibExport(int y, int m, int d, int h, int mi, double sec,
    int a, int aT, int b, int bT, double[] mat, double[] dis, string path, int pathlen);
    }
}
