using System.Text.Json;

namespace Texto_JSon_Binario;
public class PruebaFicheros
{
    public static void ConversionFicheros()
    {
        Texto_a_JSON("entradaDatos.txt", "mercancias.json");
        JSON_a_binario("mercancias.json", "mercancias.dat");
        ListarBinario("mercancias.dat");
    }

    private static void Texto_a_JSON(string nfOrigen, string nfDestino)
    {
        string pais;
        decimal peso;
        List<MercanciasJSON> mercancias = new List<MercanciasJSON>();

        try
        {
            using (StreamReader sr = new StreamReader(nfOrigen))
            {
                while ((pais = sr.ReadLine()) != null && pais != "0")
                {
                    peso = Convert.ToDecimal(sr.ReadLine());
                    mercancias.Add(new MercanciasJSON(pais, peso));
                }

                string jsonString = JsonSerializer.Serialize(mercancias, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(nfDestino, jsonString);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
        }
    }

    private static void JSON_a_binario(string nfOrigen, string nfDestino)
    {
        string jsonString = File.ReadAllText(nfOrigen);

        List<MercanciasJSON> mercancias = JsonSerializer.Deserialize<List<MercanciasJSON>>(jsonString);

        try
        {
            using (FileStream fs = new FileStream(nfDestino, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (MercanciasJSON mercancia in mercancias)
                    {
                        bw.Write(mercancia.Destino);
                        bw.Write(mercancia.Peso);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
        }
    }

    private static void ListarBinario(string nf)
    {
        if (!File.Exists(nf))
        {
            Console.WriteLine($"El archivo {nf} no existe.");
            return;
        }

        List<MercanciasJSON> mercancias = new List<MercanciasJSON>();

        using (BinaryReader br = new BinaryReader(new FileStream(nf, FileMode.Open, FileAccess.Read)))
        {
            try
            {
                // Leer todo el archivo
                while (true)
                {
                    string destino = br.ReadString();
                    decimal peso = br.ReadDecimal();

                    MercanciasJSON mercancia = new MercanciasJSON(destino, peso);
                    mercancias.Add(mercancia);
                }
            }
            catch (EndOfStreamException) { }
        }

        Console.WriteLine($"Mercancías listadas desde el archivo binario {nf}:");
        foreach (MercanciasJSON mercancia in mercancias)
        {
            Console.WriteLine(mercancia.ToString());
        }
    }

    /*
    public static List<Mercancia> A_CargarDatosTxt(out Decimal precioKilo)
    {
        String? pais;
        Decimal peso;
        List<Mercancia> mercancias = new List<Mercancia>();
        StreamReader sr = new StreamReader("datos-mercancias.txt");

        pais = sr.ReadLine();
        while (pais != null && pais != "0")
        {
            try
            {
                peso = Convert.ToDecimal(sr.ReadLine());
                mercancias.Add(new Mercancia(pais, peso));
            }
            catch (MercanciasException ex)
            {
                Console.WriteLine($"Destino o peso de mercancia es inválido\n{ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Peso debe ser un número\n{ex.Message}");
            }
            pais = sr.ReadLine();
        }

        foreach (Mercancia m in mercancias)
            Console.WriteLine(m);

        try
        {
            precioKilo = Convert.ToDecimal(sr.ReadLine());
        }
        catch (FormatException)
        {
            Console.Write("Precio/kilo: ");
            precioKilo = Convert.ToDecimal(Console.ReadLine());
        }
        sr.Close();

        return mercancias;
    }
    */

    /*
    public static void B_EscribirJson(List<Mercancia> mercancias, Decimal precioKilo)
    {
        Dictionary<string, decimal> enviosAgrupados = new Dictionary<string, decimal>();

        // Agrupar las mercancías por destino y calcular el peso acumulado por destino
        foreach (Mercancia m in mercancias)
        {
            if (enviosAgrupados.ContainsKey(m.Destino))
                enviosAgrupados[m.Destino] += m.Peso;
            else
                enviosAgrupados.Add(m.Destino, m.Peso);
        }

        List<object> jsonDataList = new List<object>();

        foreach (KeyValuePair<string, decimal> kvp in enviosAgrupados)
        {
            var jsonData = new
            {
                destino = kvp.Key,
                peso = kvp.Value
            };

            jsonDataList.Add(jsonData);
        }

        // Convertir la lista de objetos a formato JSON
        string jsonString = JsonSerializer.Serialize(jsonDataList);

        // Escribir el JSON en un archivo
        string ficheroSalida = "mercancias.json";
        File.WriteAllText(ficheroSalida, jsonString);

        Console.WriteLine($"Archivo JSON generado: {ficheroSalida}");
    }
    */

}
