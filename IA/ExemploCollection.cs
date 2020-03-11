using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA
{
    [JsonObjectAttribute]
    public class ExemploCollection : ICloneable, IReadOnlyList<Exemplo>
    {
        public List<Exemplo> Exemplos { get; set; }
        public string[] Títulos { get; set; }
        public double[] Máximos { get; set; }
        public int Entradas => Títulos.Length;

        public ExemploCollection() { Exemplos = new List<Exemplo>(); }

        public ExemploCollection(int inputs) : this()
        {
            Títulos = new string[inputs];
            Máximos = new double[inputs];
        }

        public ExemploCollection Novo(double[] entradas, bool saída)
        {
            Exemplos.Add(new Exemplo { Entradas = entradas, Saída = saída });
            return this;
        }

        public int Count => Exemplos.Count;

        public Exemplo this[int index] => Exemplos[index];

        public object Clone() =>
            new ExemploCollection
            {
                Títulos = Títulos.Clone() as string[],
                Exemplos = new List<Exemplo>(Exemplos.Select(e => e.Clone() as Exemplo))
            };

        public ExemploCollection Validar()
        {
            Console.WriteLine("Validando exemplos...");
            if (Exemplos.Count == 0)
                throw new FormatException("Objeto de exemplos está vazio");
            var lenPrimeiro = Exemplos[0].Entradas.Length;
            for (int i = 1; i < Exemplos.Count; i++)
                if (Exemplos[i].Entradas.Length != lenPrimeiro)
                    throw new FormatException("Todos os exemplos precisam ter o mesmo número de entradas");
            if (lenPrimeiro != Títulos.Length)
                throw new FormatException("A quantidade de títulos precisa ser a mesma que as entradas dos exemplos");
            return this;
        }

        public ExemploCollection Normalizar()
        {
            Console.WriteLine("Normalizando valores...");
            Máximos = new double[Entradas];
            for (int i = 0; i < Entradas; i++)
                Máximos[i] = double.MinValue;
            foreach (var expectation in Exemplos)
                for (int i = 0; i < Entradas; i++)
                    if (expectation.Entradas[i] > Máximos[i])
                        Máximos[i] = expectation.Entradas[i];
            foreach (var expectation in Exemplos)
            {
                expectation.Normalizados = new double[Entradas];
                for (int i = 0; i < Entradas; i++)
                    expectation.Normalizados[i] = Máximos[i] == 0 ? 0 : expectation.Entradas[i] / Máximos[i];
            }
            return this;
        }

        public IEnumerator<Exemplo> GetEnumerator() =>
            ((IEnumerable<Exemplo>)Exemplos).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            Exemplos.GetEnumerator();
    }
}
