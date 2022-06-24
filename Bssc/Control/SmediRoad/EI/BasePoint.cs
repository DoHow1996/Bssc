using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EI
{
    public class BasePoint
    {
        private double x;
        private double y;
        private double z;

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
    }
}
