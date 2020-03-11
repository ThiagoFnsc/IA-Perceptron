using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IA
{
    public class Pergunta
    {
        public static T Perguntar<T>(string pergunta, Func<string, T> converter, string padrao, Func<T, string> validar)
        {
            while (true)
                try
                {
                    Console.Write($"{pergunta}{(string.IsNullOrEmpty(padrao) ? "" : $" ({padrao})")}: ");
                    var linha = Console.ReadLine();
                    var convertido = converter(string.IsNullOrEmpty(linha) ? padrao : linha);
                    var validacao = validar(convertido);
                    if (string.IsNullOrEmpty(validacao))
                        return convertido;
                    else
                        throw new Exception(validacao);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }

        public static int[] PerguntarResolução() =>
            Perguntar("Digite a resolução", v =>
            {
                var partes = v.Split("x");
                return new int[] { Convert.ToInt32(partes[0]), Convert.ToInt32(partes[1]) };
            }, "3840x2160", v => v[0] >= 200 && v[1] >= 200 ? null : "X e Y precisam ser maiores ou iguals que 200");

        public static ExemploCollection PerguntarArquivoExemplos() =>
            Pergunta.Perguntar(
                "Digite o nome do arquivo de exemplos",
                r => JsonConvert.DeserializeObject<ExemploCollection>(File.ReadAllText(r)),
                new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles().Where(f => !f.Name.StartsWith("IA.")).FirstOrDefault(f => f.Extension == ".json")?.Name ?? "exemplo.json",
                _ => null);
    }
}
