using GEjercicioF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    // Factory: el ÚNICO lugar del sistema que hace new de tarifas
    public static class TarifaFactory
    {
        public static ITarifa Crear(string codigo)
        {
            switch (codigo?.Trim().ToUpper())                      // "hora ": "HORA"  si es null queda null
            {
                case "HORA": return new TarifaHora();
                case "ESTADIA": return new TarifaEstadia();
                case "NOCHE": return new TarifaNocturna();
                default: throw new ArgumentException($"Código de tarifa desconocido: {codigo}");
            }
        }

        public static List<string> CodigosDisponibles()            // para llenar un ComboBox, por ejemplo
        {
            return new List<string> { "HORA", "ESTADIA", "NOCHE" };
        }
    }
}
