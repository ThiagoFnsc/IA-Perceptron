using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{
    public class Exemplo : ICloneable
    {
        public double[] Normalizados { get; set; }
        public double[] Entradas { get; set; }
        public bool Saída { get; set; }

        public Exemplo() { }

        public string ToString(Perceptron perceptron) =>
            $"Com os valores: {perceptron.FormatarDados?.Invoke(Entradas, perceptron.Exemplos.Títulos) ?? string.Join(", ", Entradas.Select((entrada, i)=>$"{perceptron.Exemplos.Títulos[i]}: {entrada}"))} espera-se a saída {Saída}";

        public object Clone() =>
            new Exemplo
            {
                Saída=Saída,
                Normalizados=Normalizados?.Clone() as double[]??null,
                Entradas=Entradas.Clone() as double[]
            };
    }
}