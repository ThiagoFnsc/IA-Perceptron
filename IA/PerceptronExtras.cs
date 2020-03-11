using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace IA
{
    public partial class Perceptron
    {
        public void TestarConsole()
        {
            double[] entradas = new double[Entradas];
            for (int i = 0; i < Entradas; i++)
            {
                Console.Write($"Digite o valor para a entrada '{Exemplos.Títulos[i]}': ");
                entradas[i] = double.Parse(Console.ReadLine()) / Exemplos.Máximos[i];
            }
            Console.WriteLine($"Resultado: {Testar(entradas)}.");
        }

        public Perceptron Renderizar(int resX, int resY, string filename) =>
            Renderizar(Exemplos, resX, resY, filename);

        public Perceptron Renderizar(ExemploCollection exemplos, int resX, int resY, string filename)
        {
            if (Entradas != 2)
                throw new FormatException("Só é possível gerar o gráfico com um perceptron de duas entradas");
            Console.WriteLine("Gerando gráfico...");
            MagickColor
                activeC = MagickColor.FromRgb(72, 187, 133),
                inactiveC = MagickColor.FromRgb(240, 106, 133),
                pointActive = MagickColor.FromRgb(0, 255, 0),
                pointInactive = MagickColor.FromRgb(255, 0, 0);
            ushort[]
                active = new ushort[] { activeC.R, activeC.G, activeC.B },
                inactive = new ushort[] { inactiveC.R, inactiveC.G, inactiveC.B };
            using var img = new MagickImage(MagickColor.FromRgb(0, 0, 0), resX, resY);
            var pxls = img.GetPixelsUnsafe();
            var draw = new Drawables();
            double[] praTestar = new double[Entradas];
            for (int x = 0; x < img.Width; x++)
            {
                praTestar[0] = ((double)x).Map(0, img.Width, 0, 1);
                for (int y = 0; y < img.Height; y++)
                {
                    praTestar[1] = ((double)y).Map(0, img.Height, 1, 0);
                    pxls.SetPixel(x, y, Testar(praTestar) ? active : inactive);
                }
            }

            var pointSize = Math.Min(img.Width, img.Height) / 25;

            int acertos = 0;
            for (int i = 0; i < exemplos.Count; i++)
            {
                var rad = i < QuantTreinamento ? pointSize / 3 : pointSize / 10;
                if (Testar(exemplos[i]) == exemplos[i].Saída)
                    acertos++;
                draw.FillColor(exemplos[i].Saída ? pointActive : pointInactive)
                    .Ellipse(exemplos[i].Entradas[0].Map(0, exemplos.Máximos[0], 0, img.Width), exemplos[i].Entradas[1].Map(0, exemplos.Máximos[1], img.Height, 0), rad, rad, 0, 360);
            }

            var margin = 15;
            draw.FillColor(MagickColor.FromRgb(0, 0, 0))
                .Font("Consolas")
                .FontPointSize(pointSize)
                .TextAlignment(TextAlignment.Left)
                .Text(margin, img.Height - margin, "0")
                .Text(margin, margin + pointSize, exemplos.Máximos[1].ToString())
                .Text(margin, img.Height / 2, exemplos.Títulos[1])
                .TextAlignment(TextAlignment.Right)
                .Text(img.Width - margin, img.Height - margin, exemplos.Máximos[0].ToString())
                .TextAlignment(TextAlignment.Center)
                .Text(img.Width / 2, img.Height - margin, exemplos.Títulos[0])
                .Text(img.Width / 2, margin + pointSize, $"{Math.Floor(PropTreinamento * 100)}% de treinamento; {Math.Floor(((float)acertos / exemplos.Count) * 100)}% de acertos")
                .FillColor(MagickColor.FromRgb(255, 255, 255))
                .Line(margin / 2, 0, margin / 2, img.Height)
                .Line(0, img.Height - margin / 2, img.Width, img.Height - margin / 2)
                .Draw(img);
            img.Write(filename);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Process.Start("explorer", filename);
            Console.WriteLine($"Gráfico gerado e salvo no arquivo {filename}");
            return this;
        }

        public Perceptron TestarExemplos() =>
            TestarExemplos(Exemplos);

        public Perceptron TestarExemplos(ExemploCollection exemplos)
        {
            Console.WriteLine("Testando todos os exemplos");
            int corretos = 0;
            for (int i = 0; i < exemplos.Count; i++)
            {
                var res = Testar(exemplos[i]);
                if (i == QuantTreinamento)
                    Console.WriteLine("---- Não usados para o treino ----");
                var acertou = res == exemplos[i].Saída;
                if (acertou)
                    corretos++;
                Console.WriteLine($"    (Acertou: {(acertou ? "Sim" : "Não")}) {exemplos[i].ToString(this)}");
            }
            Console.WriteLine($"Acertou {corretos}/{exemplos.Count} ({Math.Floor(((float)corretos / exemplos.Count) * 100)}%)");
            return this;
        }
    }
}
