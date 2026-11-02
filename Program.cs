namespace GuiaEjercitacion2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;   // para que la consola de Windows muestre bien ° y ¡

            Titulo("B1 · Singleton: ¿cuántos contadores hay?");
            EjercicioB1.Demo.Ejecutar();                          // namespace EjercicioB1 → clase Demo → método Ejecutar

            Titulo("B2 · Strategy: la cuenta del restaurante");
            EjercicioB2.Demo.Ejecutar();

            Titulo("B3 · Observer: el canal y sus suscriptores");
            EjercicioB3.Demo.Ejecutar();

            Titulo("C1 · La alarma (corregida)");
            EjercicioC1.Demo.Ejecutar();

            Titulo("C2 · Strategy de envíos (corregida)");
            EjercicioC2.Demo.Ejecutar();

            Titulo("C3 · Factory de notificadores (corregida)");
            EjercicioC3.Demo.Ejecutar();

            Titulo("D1 · Observer: el termómetro de la cocina");
            EjercicioD1.Demo.Ejecutar();

            Titulo("D2 · Factory + Strategy: descuentos");
            EjercicioD2.Demo.Ejecutar();

            Titulo("F · Playa de estacionamiento Plaza Colón");
            EjercicioF.Demo.Ejecutar();                           // vive en EjercicioF/Program.cs
        }

        // Imprime un encabezado para separar cada ejercicio en la consola
        static void Titulo(string texto)
        {
            Console.WriteLine();                                  // linea en blanco antes
            Console.WriteLine($"========== {texto} ==========");
        }
    }
}
