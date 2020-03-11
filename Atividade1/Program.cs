using IA;
using System;

namespace Atividade1
{
    class Program
    {
        static void Main(string[] args)
        {
            var perceptron = new Perceptron(Pergunta.PerguntarArquivoExemplos());
            perceptron.PropTreinamento = .7;
            int op = 0;
            while (true)
            {
                op = Pergunta.Perguntar("\nEscolha uma opção:\n" +
                    $"  1 - Treinar\n" +
                    $"  2 - Alterar a taxa de aprendizagem ({perceptron.Magnitude})\n" +
                    $"  3 - Alterar a porcentagem de treinamento ({Math.Round(perceptron.PropTreinamento * 100).ToString()}%)\n" +
                    $"  4 - Gerar um gráfico\n" +
                    $"  5 - Testar todos os exemplos\n" +
                    $"  6 - Resetar o treinamento\n" +
                    $"  7 - Testar um novo set de valores\n" +
                    $"  8 - Slideshow de evolução\n" +
                    $"  0 - Sair\n", v => Convert.ToInt32(v), "0", _ => null);
                Console.Clear();
                switch (op)
                {
                    case 0:
                        return;
                    case 1:
                        perceptron.Treinar(Pergunta.Perguntar("Detalhar épocas?", v => v.ToLower().Contains("s"), "n", _ => null));
                        break;
                    case 2:
                        perceptron.Magnitude = Pergunta.Perguntar("Digite a nova taxa de aprendizagem", v => Convert.ToDouble(v), perceptron.Magnitude.ToString(), v => v > 0 ? null : "Precisa ser um número maior que zero");
                        break;
                    case 3:
                        perceptron.PropTreinamento = Pergunta.Perguntar("Digite a nova porcentagem de treinamento", v => Convert.ToDouble(v) / 100, Math.Round(perceptron.PropTreinamento * 100).ToString(), v => v > 0 && v <= 1 ? null : "Valor precisa ser maior que zero e menor ou igual a 100");
                        break;
                    case 4:
                        var res = Pergunta.PerguntarResolução();
                        perceptron.Renderizar(res[0], res[1], Pergunta.Perguntar("Digite o nome do arquivo de saída", t => t, $"{Math.Round(perceptron.PropTreinamento * 100)}%.png", _ => null));
                        break;
                    case 5:
                        perceptron.TestarExemplos();
                        break;
                    case 6:
                        perceptron.Reset();
                        break;
                    case 7:
                        perceptron.TestarConsole();
                        break;
                    case 8:
                        var resol = Pergunta.PerguntarResolução();
                        var incrementos = Pergunta.Perguntar("Com incrementos de quantos %?", v => Convert.ToInt32(v), "5", v => v > 0 && v <= 50 ? null : "O valor precisa ser maior que zero e menor ou igual que 50");
                        for (int i = 0; i <= 100; i = i == 100 ? int.MaxValue : Math.Min(i + incrementos, 100))
                        {
                            perceptron.PropTreinamento = (double)i / 100;
                            perceptron.Treinar().Renderizar(resol[0], resol[1], $"{i}%.png");
                        }
                        break;
                    default:
                        Console.WriteLine("Opção não reconhecida");
                        break;
                }
            }
        }
    }
}
