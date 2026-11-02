using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    // Suscriptor 3: avisa solo cuando quedan 2 lugares o menos
    public class AlertaOcupacionObserver : IPlayaObserver
    {
        private const int Limite = 2;

        public void Actualizar(EventoPlaya evento)
        {
            if (evento.LugaresLibres <= Limite)                    // 3 libres: nada · 2 libres: alerta
                Console.WriteLine($"  [Alerta] ¡Quedan {evento.LugaresLibres} lugares!");
        }
    }
}