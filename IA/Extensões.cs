using System;
using System.Collections.Generic;
using System.Text;

namespace IA
{
    public static class Extensões
    {
        public static double Map(this double x, double in_min, double in_max, double out_min, double out_max) =>
            (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

        public static int Map(this int x, int in_min, int in_max, int out_min, int out_max) =>
            (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
