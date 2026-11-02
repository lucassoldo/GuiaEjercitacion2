using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    // STRATEGY: el contrato que cumplen todas las tarifas
    public interface ITarifa
    {
        string Nombre { get; }                                     // "Por hora", "Estadia", "Nocturna"
        decimal Calcular(int minutos);                             // cuanto se cobra por esos minutos
    }
}
