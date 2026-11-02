using EjercicioF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEjercicioF
{
    // la tarifa de estadía: 6000 por las primeras 12 horas, y 1200 por cada hora extra
    public class TarifaEstadia : ITarifa
    {
        private const int MinutosIncluidos = 12 * 60;              // 720 minutos = 12 horas

        public string Nombre => "Estadía";

        public decimal Calcular(int minutos)
        {
            if (minutos <= MinutosIncluidos)                       // no se paso de las 12 horas
                return 6000m;

            int minutosExtra = minutos - MinutosIncluidos;         // 900 − 720 = 180
            int horasExtra = (int)Math.Ceiling(minutosExtra / 60.0);   // 180 / 60 = 3
            return 6000m + horasExtra * 1200m;                     // 6000 + 3600 = 9600
        }
    }
}
