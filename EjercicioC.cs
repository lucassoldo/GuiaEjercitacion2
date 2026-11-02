// ============================================
// SECCIÓN C — Pregunta 1 · La alarma que se rompe sola
// ¿Cuál es el error y de qué tipo es?: es un error de ejecucion, compila pero al correr deja de funcionar con InvalidOperationException el program se cae y Luz encendida nunca aparece
// ¿Por qué ocurre?:Sonar() recorre _observadores con foreach, en el medio AvisoUnico se da de baja y eso hace un Remove sobre la misma lista que se esta reccorriendo
//                  no se puede modificar una lista mientras un foreach la reccore,cuando va a buscar el siguiente elemento,detecta que la lista cambio y lanza la excepcion.    
// ¿Cómo se corrige?: Recorriendo una copia de la lista
// ============================================

namespace EjercicioC1
{
    public interface IAlarmaObserver
    {
        void Actualizar(Alarma alarma);
    }
    public class Alarma
    {
        private readonly List<IAlarmaObserver> _observadores = new List<IAlarmaObserver>();
        public void Suscribir(IAlarmaObserver o) => _observadores.Add(o);
        public void Desuscribir(IAlarmaObserver o) => _observadores.Remove(o);
        public void Sonar()
        {
            Console.WriteLine("¡Riiiing!");
            //foreach (var o in _observadores)  ← algo está mal acá
            foreach (var o in _observadores.ToList())// correccion
                o.Actualizar(this);
        }
    }
    public class AvisoUnico : IAlarmaObserver
    {
        public void Actualizar(Alarma alarma)
        {
            Console.WriteLine("Me avisaron una vez y me doy de baja");
            alarma.Desuscribir(this);
        }
    }
    public class Luz : IAlarmaObserver
    {
        public void Actualizar(Alarma alarma) => Console.WriteLine("Luz encendida");
    }


    public static class Demo
    {
        public static void Ejecutar()
        {
            var alarma = new Alarma();
            alarma.Suscribir(new AvisoUnico());
            alarma.Suscribir(new Luz());
            alarma.Sonar();

        }
    }
}

// ============================================
// SECCIÓN C — Pregunta 2 · Una "Strategy" sospechosa
// ¿Cuál es el error y de qué tipo es?: es un error de diseño,compila y calcula bien pero no es estrategy, es un condicional que hace todo en una clase.
// ¿Qué pasa cuando llega un tipo nuevo, por ejemplo "Bicicleta"?:bicicleta no es moto ni camion asi que cae en el return y se cobra como estandar,
//                                                                si quiero cobrarle otra cosa tengo que modificar la clase EnvioStrategy y agregar otro if, rompiendo el principio abierto/cerrado.
// ¿Cómo se corrige?: se corrigue creando una interfaz IEnvioStrategy y haciendo una clase por cada tipo de envio, cada clase implementa la interfaz y calcula a su manera,
//                    asi si llega un tipo nuevo solo se crea otra clase que implemente la interfaz y no se toca nada de lo anterior.
// ============================================
namespace EjercicioC2
{
    public interface IEnvioStrategy
    {
        decimal Calcular(decimal pesoKg);
    }
    /*public class EnvioStrategy : IEnvioStrategy
    {
        private readonly string _tipo;
        public EnvioStrategy(string tipo) => _tipo = tipo;
        public decimal Calcular(decimal pesoKg)
        {
            if (_tipo == "Moto") return 1500 + pesoKg * 100; // ← ¿esto es Strategy?
            if (_tipo == "Camion") return 8000 + pesoKg * 40;
            return 3000 + pesoKg * 70;
        }*/
        // corregido
        public class EnvioMoto : IEnvioStrategy
        {
            public decimal Calcular(decimal pesoKg) => 1500 + pesoKg * 100;   
        }

        public class EnvioCamion : IEnvioStrategy
        {
            public decimal Calcular(decimal pesoKg) => 8000 + pesoKg * 40;    
        }

        public class EnvioEstandar : IEnvioStrategy
        {
            public decimal Calcular(decimal pesoKg) => 3000 + pesoKg * 70;    
        }

        public class EnvioBicicleta : IEnvioStrategy                          // tipo NUEVO
        {
            public decimal Calcular(decimal pesoKg) => 900 + pesoKg * 150;    // formula que invente para el ejemplo
        }
 


    public static class Demo
    {
        public static void Ejecutar()
        {
            var estrategias = new List<IEnvioStrategy>                 // una lista de la INTERFAZ
             {
                new EnvioMoto(), new EnvioCamion(), new EnvioEstandar(), new EnvioBicicleta()
             };
            foreach (var e in estrategias)                             // recorro cada estrategia
                Console.WriteLine($"{e.GetType().Name}: {e.Calcular(5):0}");   // misma llamada, cada una calcula a su manera
        }
    }
}

// ============================================
// SECCIÓN C — Pregunta 3 · La factory que no compila
// ¿Cuál es el error y de qué tipo es?:es un error de compilacion, el metodo Crear no tiene un return en caso de que el canal no sea ni MAIL ni SMS,
//                                     entonces el compilador detecta que hay un camino que no retorna nada y da error.
// ¿Cómo se corrige?: lanzando una excepción en caso de que el canal no sea reconocido
// ¿Por qué conviene lanzar una excepción con un mensaje claro y no devolver null?: porque asi el error se detecta inmediatamente y se entiende la causa, en lugar de provocar un NullReferenceException más adelante
// ============================================
namespace EjercicioC3
{
    public interface INotificador
    {
        void Enviar(string destino, string mensaje);
    }
    public class NotificadorMail : INotificador
    {
        public void Enviar(string destino, string mensaje)
        {
            Console.WriteLine($"[Mail a {destino}]{mensaje}");
        }
    }
    
    public class NotificadorSms : INotificador
    {
        public void Enviar(string destino, string mensaje)
        {
            Console.WriteLine($"[SMS a {destino}]{mensaje}");
        }
    }
    
    public static class NotificadorFactory
    {
        public static INotificador Crear(string canal)
        {
            if (canal == "MAIL") return new NotificadorMail();
            if (canal == "SMS") return new NotificadorSms();
            throw new ArgumentException($"Canal desconocido: {canal}");   // excepción que faltaba por en caso de que canal no sea reconocido
        } // ← algo está mal acá
    }


    public static class Demo
    {
        public static void Ejecutar()
        {
            NotificadorFactory.Crear("MAIL").Enviar("lucas@mail.com", "Tu pedido salió");   // camino 1: MAIL
            NotificadorFactory.Crear("SMS").Enviar("223-555-1234", "Tu pedido salió");       // camino 2: SMS

            try                                                     
            {
                NotificadorFactory.Crear("PEPE");                   // camino 3: un canal que no existe
            }
            catch (ArgumentException ex)                              // "…y si salta una ArgumentException, atrapala"
            {
                Console.WriteLine("Error: " + ex.Message);            // muestro el mensaje de error
            }
        }
    }
}
