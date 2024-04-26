namespace FicheroBinario;

// Hay que linkear los proyectos
using Utilidades;

internal class Program
{
    static void Main(string[] args)
    {
        String linea;
        int num, max, min, total, cont;
        Decimal media;
        char crearNuevo = 'S';
        #region Menu
        int nOpcion;
        String nFichero = "numeros.dat";
        Menu menu = new Menu();
        menu.añadir("Crear Fichero");
        menu.añadir("Introducir números");
        menu.añadir("Valor máximo");
        menu.añadir("Valor mínimo");
        menu.añadir("Media de valores");
        menu.añadir("Salir");
        #endregion

        menu.mostrar();
        nOpcion = menu.leer();
        while (nOpcion != -1 && nOpcion != menu.getOpciones())
        {
            switch (nOpcion)
            {
                case 1:
                    Console.Write("Nombre del fichero a crear: ");
                    nFichero = Console.ReadLine();
                    if (File.Exists(nFichero))
                    {
                        Console.Write("El fichero ya existe. \nLo quiere sobreescribir? (s/n) ");
                        crearNuevo = Console.ReadLine().Trim().ToUpper()[0];
                    }
                    if (crearNuevo == 'S')
                        File.Create(nFichero).Close();
                    break;

                case 2:
                    //MemoryStream memory = new MemoryStream();
                    //using (BinaryWriter bw = new BinaryWriter(memory))
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(nFichero, FileMode.Append, FileAccess.Write)))
                    {
                        Console.WriteLine("Introduzca valores numéricos (0 para salir): ");
                        linea = Console.ReadLine().Trim();
                        while (!linea.Equals("0"))
                        {
                            try
                            {
                                int valor = Convert.ToInt32(linea);
                                bw.Write(valor);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"{linea} no es un número.");
                            }
                            linea = Console.ReadLine().Trim();
                        }
                    }
                    //foreach(byte b in memory.ToArray()) {
                    //    Console.WriteLine(b);
                    //}
                    break;
                case 3:
                    try
                    {
                        cont = 0;
                        using (BinaryReader br = new BinaryReader(new FileStream(nFichero, FileMode.Open, FileAccess.Read)))
                        {
                            max = Int32.MinValue;
                            try
                            {
                                // Lee todo el archivo
                                while (true)
                                {
                                    num = br.ReadInt32();
                                    cont = 1;
                                    if (num > max)
                                        max = num;
                                }
                            }
                            catch (EndOfStreamException) { }
                        }
                        if (cont != 0)
                            Console.WriteLine($"El número máximo de fichero '{nFichero}' es {max}.");
                        else
                            Console.WriteLine($"No hay datos en el fichero '{nFichero}'.");
                    }
                    catch (IOException)
                    {
                        Console.WriteLine($"El fichero '{nFichero}' no se encuentra.");
                    }
                    break;
                case 4:
                    try
                    {
                        cont = 0;
                        using (BinaryReader br = new BinaryReader(new FileStream(nFichero, FileMode.Open, FileAccess.Read)))
                        {
                            min = Int32.MaxValue;
                            try
                            {
                                while (true)
                                {
                                    num = br.ReadInt32();
                                    cont = 1;
                                    if (num < min)
                                        min = num;
                                }
                            }
                            catch (EndOfStreamException) { }
                        }
                        if (cont != 0)
                            Console.WriteLine($"El número mínimo de fichero '{nFichero}' es {min}.");
                        else
                            Console.WriteLine($"No hay datos en el fichero '{nFichero}'.");
                    }
                    catch (IOException)
                    {
                        Console.WriteLine($"El fichero '{nFichero}' no se encuentra.");
                    }
                    break;
                case 5:
                    try
                    {
                        using (BinaryReader br = new BinaryReader(new FileStream(nFichero, FileMode.Open, FileAccess.Read)))
                        {
                            total = cont = 0;
                            try
                            {
                                while (true)
                                {
                                    num = br.ReadInt32();
                                    total += num;
                                    cont++;
                                }
                            }
                            catch (EndOfStreamException) { }
                        }
                        if (cont != 0)
                        {
                            media = total / (decimal)cont;
                            Console.WriteLine($"La media de los números del fichero '{nFichero}' es {media:f2}.");
                        }
                        else
                            Console.WriteLine("No hay datos en el fichero '{nFichero}' para la media.");
                    }
                    catch (IOException)
                    {
                        Console.WriteLine($"El fichero '{nFichero}' no se encuentra.");
                    }
                    break;

            }
            menu.mostrar();
            nOpcion = menu.leer();
        }
    }
}
