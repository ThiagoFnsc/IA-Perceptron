using IA;
using System;

namespace Atividade3
{
    class Program
    {
        static void Main(string[] args)
        {
            var reconhecedor = new ReconhecedorDeLetras(Shared.CharL, Shared.CharT, 'L', 'T')
                .Inicializar();
            for (int i = 0; i < 100; i++)
            {
                var ruido = reconhecedor.GerarRuido();
                reconhecedor
                    .MostrarMatriz(ruido)
                    .Testar(ruido);
            }
        }
    }
}
