using EjercicioF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEjercicioF
{
    // $ 1.200 por hora o fracción
    public class TarifaHora : ITarifa
    {
        public string Nombre => "Por hora";

        public decimal Calcular(int minutos)
        {
            int horas = (int)Math.Ceiling(minutos / 60.0);         // 150 min 2.5  redondea para arriba : 3
            return horas * 1200m;                                  // 3 × 1200 = 3600
        }
    }
}
