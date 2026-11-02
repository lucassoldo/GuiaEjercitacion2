using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioF
{
    // OBSERVER: el NOTIFICADOR. Ademas usa la Factory y guarda una Strategy por vehículo.
    public class Playa
    {
        private readonly int _capacidad;                                                // 10 lugares
        private readonly Dictionary<string, ITarifa> _vehiculos = new Dictionary<string, ITarifa>();   // tarifa con la que entro cada patente
        private readonly List<IPlayaObserver> _observadores = new List<IPlayaObserver>(); // lista de INTERFACES

        public Playa(int capacidad)
        {
            _capacidad = capacidad;
        }

        public int LugaresLibres => _capacidad - _vehiculos.Count;                       // se calcula cada vez

        public void Suscribir(IPlayaObserver o) => _observadores.Add(o);                 // alta
        public void Desuscribir(IPlayaObserver o) => _observadores.Remove(o);            // baja

        public void Ingresar(string patente, string codigoTarifa)
        {
            if (string.IsNullOrWhiteSpace(patente))                                      // validación 1: patente vacía
                throw new ArgumentException("La patente no puede estar vacía.");
            patente = patente.Trim().ToUpper();                                          // paso todo a mayuscula y sin espacios

            if (_vehiculos.ContainsKey(patente))                                         // validación 2: ya esta adentro
                throw new InvalidOperationException($"El vehículo {patente} ya está en la playa.");
            if (LugaresLibres == 0)                                                      // validación 3: playa llena
                throw new InvalidOperationException("La playa está llena.");

            ITarifa tarifa = TarifaFactory.Crear(codigoTarifa);                          // texto a objeto (Strategy) usando la Factory
            _vehiculos.Add(patente, tarifa);                                             // guardo con que tarifa entro

            Notificar(new EventoPlaya(TipoEventoPlaya.Ingreso, patente, 0, LugaresLibres));   // aviso despues del cambio
        }

        public decimal Egresar(string patente, int minutos)
        {
            patente = patente?.Trim().ToUpper() ?? "";                                   // mismo formato que al entrar
            if (!_vehiculos.ContainsKey(patente))                                        // no puede salir si no entro
                throw new InvalidOperationException($"El vehículo {patente} no está en la playa.");
            if (minutos < 0)
                throw new ArgumentException("Los minutos no pueden ser negativos.");

            ITarifa tarifa = _vehiculos[patente];                                        // la tarifa con la que entro
            decimal importe = tarifa.Calcular(minutos);                                  // Strategy: cada tarifa calcula a su manera
            _vehiculos.Remove(patente);                                                  // libera el lugar

            Notificar(new EventoPlaya(TipoEventoPlaya.Egreso, patente, importe, LugaresLibres));
            return importe;
        }

        private void Notificar(EventoPlaya evento)                                       // private: solo la Playa decide cuándo avisar
        {
            foreach (var o in _observadores.ToList())                                    // recorro una copia de la lista para que no rompa si un observer se da de baja en medio del aviso
                o.Actualizar(evento);                                                    // notifico a cada observer con el evento que ocurrio
        }
    }
}