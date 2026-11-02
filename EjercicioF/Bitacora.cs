using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    public class Bitacora
    {
        private static Bitacora? _instancia;                       // regla 2: campo static con la única instancia
        private readonly List<string> _registros = new List<string>();   // los renglones anotados

        private Bitacora() { }                                     // regla 1: constructor private (nadie hace new afuera)

        public static Bitacora Instancia                           // regla 3: la puerta de entrada
        {
            get
            {
                if (_instancia == null)                            // primera vez que se pide la instancia...
                    _instancia = new Bitacora();                   // la creamos sino
                return _instancia;                                 // siempre devolvemos la misma
            }
        }

        public void Registrar(string texto)
        {
            int numero = _registros.Count + 1;                     // el proximo número de renglon
            _registros.Add($"{numero:00}. {texto}");               
        }

        public void MostrarTodo()
        {
            Console.WriteLine("--- Bitácora ---");
            foreach (var r in _registros)                          // imprimo cada renglon
                Console.WriteLine(r);
        }
    }
}
