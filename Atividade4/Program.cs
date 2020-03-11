using IA;
using System;

namespace Atividade4
{
    class Program
    {
        static void Main(string[] args)
        {
            var reconhecedor1 = new ReconhecedorDeLetras(Shared.CharL, Shared.CharT, 'L', 'T')
                .Inicializar();
            var reconhecedor2 = new ReconhecedorDeLetras(Shared.CharO, Shared.CharU, 'O', 'U')
                .Inicializar();
            for (int i = 0; i < 100; i++)
            {
                var ruido = reconhecedor1.GerarRuido();
                reconhecedor1
                    .MostrarMatriz(ruido)
                    .Testar(ruido);
                reconhecedor2.Testar(ruido);
            }
        }
    }
}
