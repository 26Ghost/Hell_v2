using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hell_v2
{
    public class Class_get_param
    {
        public double a, b,b2, a2,c,c2,ecc,ygol,etalon=0,etalon2=0,rotate=0,pre_add=0,izm_ygol=0,izm_ygol2=0;
        public bool period_par=false;

        public Class_get_param(string a1, string b1, string a2_1)
        {
            a = Convert.ToDouble(a1);
            b = Convert.ToDouble(b1);
            a2 = Convert.ToDouble(a2_1);

            ecc = Math.Sqrt(1 - (b * b) / (a * a));
            c = a * ecc;
            b2 = a2 * Math.Sqrt(1 - ecc * ecc);
            c2 = a2 * ecc;
            ygol = Math.PI;

        }
        public Class_get_param() { }
    }
}
