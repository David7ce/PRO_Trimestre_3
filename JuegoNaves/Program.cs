namespace JuegoNaves;

internal class Program
{
    static void Main(string[] args)
    {
        Naves nave = new Naves();

        Console.WriteLine("Nave creada:");
        Console.WriteLine($"Tamaño: {nave.Tamaño}");
        Console.WriteLine($"Vida: {nave.Vida}");
        Console.WriteLine($"Limite de vida: {nave.LimiteVida}");
        Console.WriteLine($"Disparo: {nave.Disparo}");
        Console.WriteLine($"Escudo: {nave.Escudo}");

        // Realizar algunas acciones con la nave
        nave.AumentarVida(50);
        nave.MejorarNave();
        nave.MejorarDisparo();
        nave.MejorarEscudo();

        Console.WriteLine("\nNave después de algunas mejoras:");
        Console.WriteLine($"Tamaño: {nave.Tamaño}");
        Console.WriteLine($"Vida: {nave.Vida}");
        Console.WriteLine($"Limite de vida: {nave.LimiteVida}");
        Console.WriteLine($"Disparo: {nave.Disparo}");
        Console.WriteLine($"Escudo: {nave.Escudo}");

        // Atacar la nave
        nave.Atacar(50);

        Console.WriteLine("\nNave después de ser atacada:");
        Console.WriteLine($"Vida: {nave.Vida}");

        // Atacar hasta destruir la nave
        while (nave.Vida > 0)
        {
            nave.Atacar(100);
            Console.WriteLine($"Vida: {nave.Vida}");
        }
    }
}
