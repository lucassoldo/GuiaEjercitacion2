using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    // Suscriptor 2: la caja suma lo cobrado en cada egreso
    public class CajaObserver : IPlayaObserver
    {
        private decimal _recaudado = 0;                            // total cobrado en el dia

        public void Actualizar(EventoPlaya evento)
        {
            if (evento.Tipo != TipoEventoPlaya.Egreso)             // en un ingreso no se cobra nada
                return;

            _recaudado += evento.Importe;                          // acumulo
            Console.WriteLine($"  [Caja] Cobrado {Formato.Pesos(evento.Importe)} a {evento.Patente}. Recaudado: {Formato.Pesos(_recaudado)}");
        }
    }
}
