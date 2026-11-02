using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

namespace EjercicioF
{
    // Clase de ayuda para formatear montos en pesos
    public static class Formato
    {
        private static readonly CultureInfo Ar = new CultureInfo("es-AR");   // punto para los miles

        public static string Pesos(decimal monto) => "$ " + monto.ToString("N0", Ar);   // N0 = sin decimales
    }
}
