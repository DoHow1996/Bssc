using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI
{
    public class EICDPoint : BasePoint
    {
        private double mark;  //桩号
        private double azimuth; //方位角
        private double bulge; //凸度

        public double Mark { get => mark; set => mark = value; }
        public double Azimuth { get => azimuth; set => azimuth = value; }
        public double Bulge { get => bulge; set => bulge = value; }
    }
}
