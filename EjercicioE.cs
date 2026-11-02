namespace EjercicioE
{
    // ============================================
    // SECCIÓN E — ¿Qué patrón usarías? 
    // ============================================

    // E1: Strategy.
    // Porque: hay varias formas de calcular el costo de envio y viene una nueva forma de envio.

    // E2: Observer.    
    // Porque: un cambio "listo" tiene que llegarle a varias personas y en un momento va a sumar avisar por whatsapp al cliente tambien.

    // E3: Singleton.  
    // Porque: la configuracion tiene que ser una sola pero cada formulario hacer su propio new() y por eso el cambio en una pantalla no llega a las otras.

    // E4: Factory.
    // Porque: el medio de pago viene como texto en el json y hay que convertirlo en el objeto.

    // E5: ninguno.
    // Porque: el repositorio se crea una vez y nunca cambia.

}
