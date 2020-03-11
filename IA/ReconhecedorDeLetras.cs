using System;
using System.Collections.Generic;
using System.Text;

namespace IA
{
    public class ReconhecedorDeLetras
    {
        public Perceptron Perceptron { get; set; }
        public double[] Exemplo1 { get; set; }
        public double[] Exemplo2 { get; set; }
        public char Letra1 { get; set; }
        public char Letra2 { get; set; }

        public ReconhecedorDeLetras(double[] exemplo1, double[] exemplo2, char letra1, char letra2)
        {
            Exemplo1 = exemplo1;
            Exemplo2 = exemplo2;
            Letra1 = letra1;
            Letra2 = letra2;
        }

        public ReconhecedorDeLetras Inicializar()
        {
            ExemploCollection exemplos = new ExemploCollection(12);
            for (int y = 0, i = 0; y < 4; y++)
                for (int x = 0; x < 3; x++)
                    exemplos.Títulos[i++] = $"{x}x{y}";
            exemplos.Novo(Exemplo1, true);
            exemplos.Novo(Exemplo2, false);
            Perceptron = new Perceptron(exemplos)
            {
                FormatarDados = Shared.FormatarGrid3x12
            }
                .Treinar(true);
            return this;
        }

        public ReconhecedorDeLetras MostrarMatriz(double[] entradas)
        {
            Console.Write("\n"+Perceptron.FormatarDados(entradas, null));
            return this;
        }

        public ReconhecedorDeLetras Testar(double[] entradas)
        {
            Console.WriteLine($"Entre {Letra1} e {Letra2}, reconheceu: {(Perceptron.Testar(entradas) ? Letra1 : Letra2)}");
            return this;
        }

        public double[] GerarRuido()
        {
            var buff = new double[Perceptron.Entradas];
            var rand = new Random();
            for (int j = 0; j < Perceptron.Entradas; j++)
                buff[j] = rand.NextDouble() > .5 ? 1 : 0;
            return buff;
        }
    }
}
