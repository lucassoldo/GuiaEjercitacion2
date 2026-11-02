using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    public enum TipoEventoPlaya { Ingreso, Egreso }                // los dos cambios que se avisan

    public class EventoPlaya
    {
        public TipoEventoPlaya Tipo { get; }                       // ingreso o salio?
        public string Patente { get; }                             // que vehiculo
        public decimal Importe { get; }                            // lo cobrado 
        public int LugaresLibres { get; }                          // como quedo la playa despues del cambio

        public EventoPlaya(TipoEventoPlaya tipo, string patente, decimal importe, int lugaresLibres)
        {
            Tipo = tipo;
            Patente = patente;
            Importe = importe;
            LugaresLibres = lugaresLibres;
        }
    }
}
