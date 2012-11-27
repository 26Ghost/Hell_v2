using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Hell_v2
{
    public class Class_orbit_parametrs
    {
        public List<double[]> system1_inf = new List<double[]>();
        public List<double[]> system2_inf = new List<double[]>();

        public void convert_params(double rad,double speed,double lambda,double x,double y,double speed_y,double tg)
        {
            double[] mass=new double[7];
            mass[0] = rad;
            mass[1] = speed;
            mass[2] = lambda;
            mass[3] = x;
            mass[4] = y;
            mass[5] = speed_y;
            mass[6] = tg;
            system1_inf.Add(mass);
        }
        public void convert_params(double rad, double speed, double lambda, double x, double y, double speed_y,double tg,string str)
        {
            double[] mass = new double[7];
            mass[0] = rad;
            mass[1] = speed;
            mass[2] = lambda;
            mass[3] = x;
            mass[4] = y;
            mass[5] = speed_y;
            mass[6] = tg;
            system2_inf.Add(mass);
        }
    }
}
