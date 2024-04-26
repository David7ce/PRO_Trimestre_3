using System;

namespace Texto_JSon_Binario;

public class MercanciasJSON
{
    public const decimal PESO_MAX_CONTAINER = 1200;

    public string Destino { get; set; }
    public decimal Peso { get; set; }

    // Constructor sin parámetros requerido para la deserialización
    public MercanciasJSON()
    {
        // Puedes inicializar propiedades si es necesario
    }

    public MercanciasJSON(string pais, decimal peso)
    {
        if (string.IsNullOrWhiteSpace(pais))
            throw new Exception("Dato de destino inválido");
        if (peso <= 0 || peso > PESO_MAX_CONTAINER)
            throw new Exception("Datos de peso a transportar inválido");
        Destino = pais;
        Peso = peso;
    }

    public override string ToString()
    {
        return $"Mercancía destinada a {Destino} [{Peso} Kg.]";
    }
}
