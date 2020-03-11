using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{
    public partial class Perceptron
    {
        public double Bias { get; set; }
        public ExemploCollection Exemplos { get; set; }
        public double[] Pesos { get; set; }
        public int Entradas => Pesos.Length;
        public double Magnitude { get; set; } = .01;
        public double PropTreinamento { get; set; } = 1;
        public int QuantTreinamento => (int)(PropTreinamento * Exemplos.Count);
        public Func<double[], string[], string> FormatarDados { get; set; }

        public Perceptron(int inputs)
        {
            Pesos = new double[inputs];
            Reset();
        }

        public Perceptron(ExemploCollection exemplos) : this(exemplos.Exemplos[0].Entradas.Length)
        {
            Exemplos = exemplos.Normalizar().Validar();
        }

        public Perceptron Treinar(bool verbose = false)
        {
            Console.WriteLine($"Treinando perceptron usando os {QuantTreinamento} primeiros exemplos");
            int epoca = 0, erroTotal;
            var paraTreinar = Exemplos.Take(QuantTreinamento).ToArray();
            var início = DateTime.Now;
            while (true)
            {
                erroTotal = 0;
                foreach (var exemplo in paraTreinar)
                {
                    var erro = Sinal(exemplo.Saída) - Sinal(Testar(exemplo.Normalizados));
                    if (verbose)
                        Console.WriteLine($"    Acertou: {(erro == 0 ? "Sim" : "Não")}: {exemplo.ToString(this)}");
                    Bias += Magnitude * erro;
                    for (int i = 0; i < Entradas; i++)
                        Pesos[i] += Magnitude * erro * exemplo.Normalizados[i];
                    erroTotal += erro == 0 ? 0 : 1;
                }
                if (verbose)
                    Console.WriteLine($"Época {epoca}, Total de erros: {erroTotal}\nBias: {Bias.ToString("0.00")}\nPesos: {string.Join(", ", Pesos.Select(p=>p.ToString("0.00")))}");
                if (erroTotal == 0) break;
                else epoca++;
            }
            Console.WriteLine($"Treinamento completo! Levou {epoca} épocas e {(DateTime.Now - início).TotalSeconds} segundos.");
            return this;
        }

        public bool Testar(Exemplo exemplo) =>
            Testar(exemplo.Normalizados);

        public bool Testar(double[] entradas)
        {
            double soma = Bias;
            for (int i = 0; i < entradas.Length; i++)
                soma += entradas[i] * Pesos[i];
            return soma >= 0;
        }

        public void Reset()
        {
            var rdg = new Random();
            double random() => rdg.NextDouble() * 2 - 1;
            Bias = random();
            for (int i = 0; i < Entradas; i++)
                Pesos[i] = random();
        }

        public int Sinal(bool ativo) =>
            ativo ? 1 : -1;
    }
}