using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{
    public class Shared
    {
        public static string FormatarGrid3x12(double[] entradas, string[] títulos) =>
            "\n"+string.Join("", entradas.Select((entrada, i) => $"{(entrada == 1 ? "O" : ".")}{((i + 1) % 3 == 0 ? "\n" : " ")}"));

        public static double[] CharT = new double[] {
                1, 1, 1,
                0, 1, 0,
                0, 1, 0,
                0, 1, 0 };

        public static double[] CharL = new double[] {
                1, 0, 0,
                1, 0, 0,
                1, 0, 0,
                1, 1, 1 };

        public static double[] CharO = new double[] {
                0, 1, 0,
                1, 0, 1,
                1, 0, 1,
                0, 1, 0 };

        public static double[] CharU = new double[] {
                1, 0, 1,
                1, 0, 1,
                1, 0, 1,
                1, 1, 1 };
    }
}
