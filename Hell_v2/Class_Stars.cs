using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hell_v2
{
    public class Class_Stars
    {
        public double mass, rad, speed, lambda, x, y, speed_y,tg;

        public Class_Stars(double mass1, double speed1, double rad1, double x1, double y1,double tg1)
        {
            mass = mass1;
            rad = rad1;
            speed = speed1;
            x = x1;
            y = y1;
            tg = tg1;
        }
        public Class_Stars() { }

    }
}
