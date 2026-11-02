using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    // Suscriptor 1: la pantalla de la entrada
    public class PantallaLugares : IPlayaObserver
    {
        public void Actualizar(EventoPlaya evento)
        {
            Console.WriteLine($"  [Pantalla] Lugares libres: {evento.LugaresLibres}");
        }
    }
}
