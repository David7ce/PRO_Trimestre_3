namespace MercanciasFichero;
//namespace Principal;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using TransporteContainers;

public class Fases
{
    /// <summary>
    /// Solicita los datos de mercancías por consola
    /// </summary>
    /// <param name="precioKilo"></param>
    /// <returns>Listado de mercancías</returns>
    public static List<Mercancia> A_CargarDatosConsola(out Decimal precioKilo)
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

    /// <summary>
    /// Carga los datos de merancías desde un archivo de texto
    /// </summary>
    /// <param name="precioKilo"></param>
    /// <returns>Listao de mercancías</returns>
    public static List<Mercancia> A_CargarDatosTxt(out Decimal precioKilo)
    {
        String? pais;
        Decimal peso;
        ListaMercancias mercancias = new ListaMercancias("datos-mercancias.txt", out precioKilo);
        foreach (Mercancia m in mercancias)
            Console.WriteLine(m);
        return mercancias;
    }

    /// <summary>
    /// Imprime por consola los datos de mercancías procesados en forma de tabla
    /// </summary>
    /// <param name="mercancias"></param>
    /// <param name="precioKilo"></param>
    public static void B_EscribirConsola(List<Mercancia> mercancias, Decimal precioKilo)
    {
        Decimal precioTotal;
        Dictionary<String, Decimal> enviosAgrupados = new Dictionary<String, Decimal>();
        int nConten;
        const int PRECIO_CONTAINER = 100;
        String lineaDeDetalle = "{0,-20}{1,-20}{2,-15}{3,-20}";

        foreach (Mercancia m in mercancias)
        {
            if (enviosAgrupados.ContainsKey(m.Destino))
                enviosAgrupados[m.Destino] = enviosAgrupados[m.Destino] + m.Peso;
            else
                //enviosAgrupados[m.Destino] = m.Peso;
                enviosAgrupados.Add(m.Destino, m.Peso);
        }

        Console.WriteLine("\nLISTADO DE DESTINOS Y CONTENEDORES");
        Console.WriteLine("----------------------------------");
        Console.WriteLine(lineaDeDetalle, "Destino", "Peso Acumulado", "Nº contenedores", "Precio total");

        foreach (String dest in enviosAgrupados.Keys)
        {
            nConten = (int)Math.Ceiling(enviosAgrupados[dest] / Mercancia.PESO_MAX_CONTAINER);
            precioTotal = nConten * PRECIO_CONTAINER + precioKilo * enviosAgrupados[dest];
            Console.WriteLine(lineaDeDetalle, dest, enviosAgrupados[dest], nConten, precioTotal);
        }
        Console.WriteLine($"\nPrecio por kilo en el contenedor {precioKilo}");
        Console.WriteLine($"Precio por contenedor {PRECIO_CONTAINER}");
        Console.WriteLine($"Peso por contenedor {Mercancia.PESO_MAX_CONTAINER}");
    }

    public static void B_EscribirHtml_Block(List<Mercancia> mercancias, decimal precioKilo)
    {
        Dictionary<string, decimal> enviosAgrupados = new Dictionary<string, decimal>();
        const int PRECIO_CONTAINER = 100;
        string nombreFichero = "mercancias.html";

        // Agrupar las mercancías por destino y acumular el peso por destino
        foreach (Mercancia m in mercancias)
        {
            if (enviosAgrupados.ContainsKey(m.Destino))
                enviosAgrupados[m.Destino] += m.Peso;
            else
                enviosAgrupados.Add(m.Destino, m.Peso);
        }

        // Crear y escribir el contenido HTML en el archivo
        using (StreamWriter sw = new StreamWriter(nombreFichero))
        {
            sw.WriteLine("<!DOCTYPE html>");
            sw.WriteLine("<html>");
            sw.WriteLine("<head>");
            sw.WriteLine("<style>");
            sw.WriteLine("table, th, td { border: 1px solid black; border-collapse: collapse; padding: 8px; }");
            sw.WriteLine("</style>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");
            sw.WriteLine("<h1>LISTADO DE DESTINOS Y CONTENEDORES</h1>");
            sw.WriteLine("<table>");
            sw.WriteLine("<tr>");
            sw.WriteLine("<th>Destino</th><th>Peso Acumulado</th><th>Nº contenedores</th><th>Precio total</th>");
            sw.WriteLine("</tr>");

            foreach (KeyValuePair<string, decimal> kvp in enviosAgrupados)
            {
                string destino = kvp.Key;
                decimal pesoAcumulado = kvp.Value;
                int nContenedores = (int)Math.Ceiling(pesoAcumulado / Mercancia.PESO_MAX_CONTAINER);
                decimal precioTotal = nContenedores * PRECIO_CONTAINER + precioKilo * pesoAcumulado;

                sw.WriteLine("<tr>");
                sw.WriteLine($"<td>{destino}</td><td>{pesoAcumulado}</td><td>{nContenedores}</td><td>{precioTotal}</td>");
                sw.WriteLine("</tr>");
            }

            sw.WriteLine("</table>");
            sw.WriteLine($"<p>Precio por kilo en el contenedor {precioKilo}</p>");
            sw.WriteLine($"<p>Precio por contenedor {PRECIO_CONTAINER}</p>");
            sw.WriteLine($"<p>Peso por contenedor {Mercancia.PESO_MAX_CONTAINER}</p>");

            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
        }
    }

    public static void B_EscribirHtml_Format(List<Mercancia> mercancias, Decimal precioKilo)
    {
        Decimal precioTotal;
        Dictionary<String, Decimal> enviosAgrupados = new Dictionary<String, Decimal>();
        int nConten;
        const int PRECIO_CONTAINER = 100;
        String lineaDeDetalle, cabecera;
        String nombreFichero = "mercancias.html";

        foreach (Mercancia m in mercancias)
        {
            if (enviosAgrupados.ContainsKey(m.Destino))
                enviosAgrupados[m.Destino] = enviosAgrupados[m.Destino] + m.Peso;
            else
                //enviosAgrupados[m.Destino] = m.Peso;
                enviosAgrupados.Add(m.Destino, m.Peso);
        }
        using (StreamWriter sw = new StreamWriter(nombreFichero))
        {
            cabecera = "    <TH>{0}</TH><TH>{1}</TH><TH>{2}</TH><TH>{3}</TH>";
            lineaDeDetalle = "    <TD>{0}</TD><TD>{1}</TD><TD>{2}</TD><TD>{3}</TD>";
            const String HTML_previo = """
            <!DOCTYPE html>
            <HTML>
            
            <HEAD>
            <STYLE>
               table, th, td {
                  border: 1px solid black;
                  border-collapse: collapse;
                  }
            </STYLE>
            </HEAD>
            
            <BODY>
            """;
            const String HTML_posterior = """
            </BODY>
            </HTML>
            """;

            sw.WriteLine(HTML_previo);
            sw.WriteLine("\n<H1>LISTADO DE DESTINOS Y CONTENEDORES</H1>");
            sw.WriteLine("<TABLE>");
            sw.WriteLine("  <TR>");
            sw.WriteLine(cabecera, "Destino", "Peso Acumulado", "Nº contenedores", "Precio total");
            sw.WriteLine("  </TR>");
            foreach (String dest in enviosAgrupados.Keys)
            {
                nConten = (int)Math.Ceiling(enviosAgrupados[dest] / Mercancia.PESO_MAX_CONTAINER);
                precioTotal = nConten * PRECIO_CONTAINER + precioKilo * enviosAgrupados[dest];

                sw.WriteLine("  <TR>");
                sw.Write(lineaDeDetalle, dest, enviosAgrupados[dest], nConten, precioTotal);
                sw.WriteLine("  </TR>");
            }
            sw.WriteLine("</TABLE>");
            sw.WriteLine("<P>");
            sw.WriteLine($" Precio por kilo en el contenedor {precioKilo}");
            sw.WriteLine("</P>\n<P>");
            sw.WriteLine($"Precio por contenedor {PRECIO_CONTAINER}");
            sw.WriteLine("</P>\n<P>");
            sw.WriteLine($"Peso por contenedor {Mercancia.PESO_MAX_CONTAINER}");
            sw.WriteLine("</P>");
            sw.WriteLine(HTML_posterior);
            sw.Close();
        }

        // Lanzar el navegador para ver resultado.
        Process.Start(
            @"C:\Program Files\Mozilla Firefox\firefox.exe",
            $"\"{nombreFichero}\""
        );
    }

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


    public static void B_EscribirBinario(List<Mercancia> mercancias, decimal precioKilo)
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

        // Nombre del archivo binario de salida
        string nombreFichero = "mercancias.dat";

        // Crear un flujo de salida para escribir en el archivo binario
        using (FileStream fs = new FileStream(nombreFichero, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                foreach (KeyValuePair<string, decimal> kvp in enviosAgrupados)
                {
                    string destino = kvp.Key;
                    decimal pesoAcumulado = kvp.Value;

                    bw.Write(destino + ": " + pesoAcumulado);
                }
            }
        }

        Console.WriteLine($"\nArchivo binario generado: {nombreFichero}");
    }
}