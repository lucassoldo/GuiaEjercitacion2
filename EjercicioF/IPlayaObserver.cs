using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    // OBSERVER: la interfaz suscriptora. La Playa solo conoce esto.
    public interface IPlayaObserver
    {
        void Actualizar(EventoPlaya evento);
    }
}
