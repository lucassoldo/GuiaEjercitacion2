using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    // Suscriptor 4: anota todo en la Bitacora (el Singleton)
    public class BitacoraObserver : IPlayaObserver
    {
        public void Actualizar(EventoPlaya evento)
        {
            if (evento.Tipo == TipoEventoPlaya.Ingreso)
                Bitacora.Instancia.Registrar($"Ingresó {evento.Patente}");
            else
                Bitacora.Instancia.Registrar($"Egresó {evento.Patente} - {Formato.Pesos(evento.Importe)}");
        }
    }
}
