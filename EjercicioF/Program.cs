// ============================================
// SECCIÓN F — Justificación (ítem h)
// SINGLETON (Bitacora): la playa necesita una sola bitacora porque si  cada clase hiciera su propio new, los
//   ingresos quedarian anotados en una y los egresos en otra, y MostrarTodo mostraria la mitad.
// STRATEGY (ITarifa + TarifaHora/Estadia/Nocturna): hay tres formas de cobrar lo mismo sin estrategy egresar
//   tendria un switch por codigo de tarifa, y cada tarifa nueva (MOTO) obligaria a abrir Playa.
// FACTORY (TarifaFactory): la tarifa llega como texto ("HORA") que elige el operador y factory es el unico
//   lugar que traduce ese texto en el objeto sin ella habría new TarifaX repartidos y nadie controlaria
//   codigos invalidos como "SEMANAL".
// OBSERVER (Playa + IPlayaObserver): cuando entra o sale un auto se enteran la pantalla, la caja, la alerta
//   y la bitácora sin Observer Playa llamaria a cada una a mano y cada aviso nuevo. 
// ============================================
namespace EjercicioF
{
    public static class Demo                                       // demo de la ejecución de la playa con todos los patrones
    {                                                             
        public static void Ejecutar()
        {
            // 1) Armo el sistema: el notificador y sus suscriptores
            var playa = new Playa(10);
            var pantalla = new PantallaLugares();                  // la guardo en una variable para poder darla de baja
            playa.Suscribir(pantalla);
            playa.Suscribir(new CajaObserver());
            playa.Suscribir(new AlertaOcupacionObserver());
            playa.Suscribir(new BitacoraObserver());

            // 2) Ingresan 8 vehículos con distintos códigos de tarifa
            playa.Ingresar("AB123CD", "HORA");
            playa.Ingresar("AC456EF", "ESTADIA");
            playa.Ingresar("AD789GH", "NOCHE");
            playa.Ingresar("AE111AA", "HORA");
            playa.Ingresar("AF222BB", "ESTADIA");
            playa.Ingresar("AG333CC", "NOCHE");
            playa.Ingresar("AH444DD", "HORA");
            playa.Ingresar("AI555EE", "HORA");                     // quedan 2 da la alerta

            // 3) Egresos (cada uno cobra con la tarifa con la que entro)
            playa.Egresar("AB123CD", 150);                         // HORA, 2 h 30 min → $ 3.600
            playa.Egresar("AC456EF", 900);                         // ESTADIA, 15 h → $ 9.600
            playa.Desuscribir(pantalla);                           // baja en ejecución: la pantalla deja de mostrar
            playa.Egresar("AD789GH", 600);                         // NOCHE → $ 4.500

            // 4) Codigo inválido: la Factory lanza la excepcion
            try { playa.Ingresar("AZ999ZZ", "SEMANAL"); }
            catch (ArgumentException ex) { Console.WriteLine("Error: " + ex.Message); }

            // 5) La bitácora única (Singleton)
            Bitacora.Instancia.MostrarTodo();

            // 6) Extra: pruebo las otras validaciones de Ingresar (en una playa chica, sin suscriptores)
            Console.WriteLine("--- Extra: validaciones ---");
            var chica = new Playa(1);
            chica.Ingresar("ZZ000ZZ", "NOCHE");
            try { chica.Ingresar("ZZ000ZZ", "HORA"); }             // repetida
            catch (InvalidOperationException ex) { Console.WriteLine("Error: " + ex.Message); }
            try { chica.Ingresar("ZZ111ZZ", "HORA"); }             // llena
            catch (InvalidOperationException ex) { Console.WriteLine("Error: " + ex.Message); }
            try { chica.Ingresar("   ", "HORA"); }                 // vacía
            catch (ArgumentException ex) { Console.WriteLine("Error: " + ex.Message); }
        }
    }
}
