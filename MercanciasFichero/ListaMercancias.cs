namespace TransporteContainers;

public class ListaMercancias : List<Mercancia>
{
    public ListaMercancias(String nFich, out Decimal precioKilo)
    {
        String? pais;
        Decimal peso;
        using (StreamReader sr = new StreamReader(nFich))
        {
            pais = sr.ReadLine();

            while (pais != null && pais != "0")
            {
                try
                {
                    peso = Convert.ToDecimal(sr.ReadLine());
                    Add(new Mercancia(pais, peso));
                }
                catch (MercanciasException ex)
                {
                    Console.WriteLine($"Destino o peso de mercancía inválido\n{ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Peso debe ser un número\n{ex.Message}");
                }
                pais = sr.ReadLine();
            }
            try
            {
                precioKilo = Convert.ToDecimal(sr.ReadLine());
            }
            catch (FormatException)
            {
                precioKilo = 0;
                Console.WriteLine($"Precio/kg. erróneo.\n");
            }
        }
    }
}