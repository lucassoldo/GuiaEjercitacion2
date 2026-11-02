// ============================================
// SECCIÓN B — Pregunta 1 · Singleton: ¿cuántos contadores hay?
// QUE IMPRIME :creando contador
//              1
//              2
//              3
//              true
// ¿Cuántas veces aparece "Creando contador" y por qué?: aparece una sola vez porque el constructor se ejecuta la primera vez que alguien llama a Contador.Instancia,
//                                                      cuando _instancia todavia es null
// ¿Por qué b.Siguiente() no devuelve 1?: Porque a y b no son dos contadores,son dos nombres para el mismo objeto, a.Siguiente() ya llevo _valor a 1, entonces b.siguiente()
//                                        lo lleva a 2,por ese motivo ReferenceEquals(a,b) devuelve true.
// ============================================
namespace EjercicioB1
{
    public class Contador
    {
        private static Contador _instancia;
        private int _valor = 0;
        private Contador() { Console.WriteLine("creando contador"); }
        public static Contador Instancia { get
            {
                if (_instancia == null)
                    _instancia = new Contador();
                return _instancia;
            } 
        }
        public int Siguiente()
        {
            _valor++;
            return _valor;
        }
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            var a = Contador.Instancia;
            var b = Contador.Instancia;
            Console.WriteLine(a.Siguiente());
            Console.WriteLine(b.Siguiente());
            Console.WriteLine(Contador.Instancia.Siguiente());
            Console.WriteLine(ReferenceEquals(a, b));
        }
    }
}

// ============================================
// SECCIÓN B — Pregunta 2 · Strategy: la cuenta del restaurante
// IMPRIME: Sin propina: 20000
//          10%: 22000
//          Fija: 21500
// ¿Quién es el contexto y quiénes son las estrategias?:el contexto es Cuenta que guarda una Ipropina y delega el calculo de la  propina a la estrategia que se le pase, 
//                                                      las estrategias son SinPropina, Propina10 y PropinaFija que implementan Ipropina
// ¿Qué hubo que modificar de Cuenta para usar PropinaFija? ¿Qué principio se cumple?: No hubo que modificar nada de Cuenta, se cumple el principio de abierto/cerrado (Open/Closed Principle)
// ============================================
namespace EjercicioB2
{
    public interface IPropina
    {
        string Nombre { get; }
        decimal Calcular(decimal total);
    }
    public class SinPropina : IPropina
    {
        public string Nombre => "Sin propina";
        public decimal Calcular(decimal total) => 0;
    }
    public class Propina10 : IPropina
    {
        public string Nombre => "10%";
        public decimal Calcular(decimal total) => total * 0.10m;
    }
    public class PropinaFija : IPropina
    {
        private readonly decimal _monto;
        public PropinaFija(decimal monto) => _monto = monto;
        public string Nombre => "Fija";
        public decimal Calcular(decimal total) => _monto;
    }
    public class Cuenta
    {
        private IPropina _propina;
        public Cuenta(IPropina propina) => _propina = propina;
        public void CambiarPropina(IPropina propina) => _propina = propina;
        public void Mostrar(decimal total)
        {
            decimal final = total + _propina.Calcular(total);
            Console.WriteLine($"{_propina.Nombre}: {final:0}");
        }
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            var cuenta = new Cuenta(new SinPropina());
            cuenta.Mostrar(20000);
            cuenta.CambiarPropina(new Propina10());
            cuenta.Mostrar(20000);
            cuenta.CambiarPropina(new PropinaFija(1500));
            cuenta.Mostrar(20000);
        }
    }
}

// ============================================
// SECCIÓN B — Pregunta 3 · Observer: el canal y sus suscriptores
// IMPRIME:Nuevo video: Patrones en C#
//          Lu mira Patrones en C#
//          Tomi mira Patrones en C#
//          Avisos registrados: 1
//          Nuevo video: Observer en WinForms
//          Tomi mira Observer en WinForms
//          Avisos registrados: 2
// ¿Por qué Lu no aparece en la segunda publicación?:porque en el program se llamo a canal.Desuscribir(lu) osea que salio de la lista _suscriptores.
//                                                   Publicar solo le avisa a los ques estan en la lista.
// ¿Canal conoce la clase Fan? ¿Cómo te das cuenta?: No,en la clase canal nunca aparece Fan,canal solo conoce la interfaz,quien conoce a fan es Program.cs que es el que arma todo.
// ============================================
namespace EjercicioB3
{
    public interface IObservador
    {
        void Actualizar(string video);
    }
    public class Canal
    {
        private readonly List<IObservador> _suscriptores = new List<IObservador>();
        public void Suscribir(IObservador o) => _suscriptores.Add(o);
        public void Desuscribir(IObservador o) => _suscriptores.Remove(o);
        public void Publicar(string video)
        {
            Console.WriteLine($"Nuevo video: {video}");
            foreach (var s in _suscriptores.ToList())
                s.Actualizar(video);
        }
    }
    public class Fan : IObservador
    {
        private readonly string _nombre;
        public Fan(string nombre) => _nombre = nombre;
        public void Actualizar(string video) => Console.WriteLine($" {_nombre} mira {video}");
    }
    public class Estadisticas : IObservador
    {
        private int _avisos = 0;
        public void Actualizar(string video)
        {
            _avisos++;
            Console.WriteLine($" Avisos registrados: {_avisos}");
        }
    }

    public static class Demo
    {
        public static void Ejecutar()
        {
            var canal = new Canal();
            var lu = new Fan("Lu");
            canal.Suscribir(lu);
            canal.Suscribir(new Fan("Tomi"));
            canal.Suscribir(new Estadisticas());
            canal.Publicar("Patrones en C#");
            canal.Desuscribir(lu);
            canal.Publicar("Observer en WinForms");
        }
    }
}
