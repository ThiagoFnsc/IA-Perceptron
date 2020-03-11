using IA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Atividade2
{
    class Program
    {
        static void Main(string[] args)
        {
            ExemploCollection exemplos = Pergunta.PerguntarArquivoExemplos().Normalizar();
            var res = Pergunta.PerguntarResolução();
            ExemploCollection
                exemplosBons = exemplos.Clone() as ExemploCollection,
                exemplosMaus = exemplos.Clone() as ExemploCollection;
            exemplosBons.Exemplos = new List<Exemplo>(exemplosBons.Where(e => e.Saída == true));
            exemplosMaus.Exemplos = new List<Exemplo>(exemplosMaus.Where(e => e.Saída == false));
            Perceptron
                bons = new Perceptron(exemplosBons),
                maus = new Perceptron(exemplosMaus);
            bons.Exemplos.Máximos = exemplos.Máximos;
            maus.Exemplos.Máximos = exemplos.Máximos;
            Console.WriteLine("------- Perceptron apenas com exemplos bons -------");
            bons
                .Treinar()
                .TestarExemplos()
                .Renderizar(res[0], res[1], "bons.png");
            Console.WriteLine("------- Perceptron apenas com exemplos ruins -------");
            maus
                .Treinar()
                .TestarExemplos()
                .Renderizar(res[0], res[1], "maus.png");

            Console.WriteLine("------- Perceptron apenas com exemplos bons com dados ruins juntos -------");
            var bonsTemp = new List<Exemplo>((IEnumerable<Exemplo>)bons.Exemplos.Exemplos.ToArray().Clone());
            bons.Exemplos.Exemplos = new List<Exemplo>(bons.Exemplos.Exemplos.Concat(maus.Exemplos.Exemplos));
            bons.TestarExemplos();
            bons.Renderizar(res[0], res[1], "todos os exemplos quando treinado com bons.png");

            Console.WriteLine("------- Perceptron apenas com exemplos ruins com dados bons juntos -------");
            maus.Exemplos.Exemplos = new List<Exemplo>(maus.Exemplos.Exemplos.Concat(bonsTemp));
            maus.TestarExemplos();
            maus.Renderizar(res[0], res[1], "todos os exemplos quando treinado com ruins.png");
        }
    }
}
