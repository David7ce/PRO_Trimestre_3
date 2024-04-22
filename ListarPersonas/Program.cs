namespace ListarPersonas;

using ListaPlana;
using ListaJSON;

internal class Program
{
    static void Main(string[] args)
    {
        // Crear una instancia de ListaPersona, que carga los datos
        ListaJSON.ListaPersona listaJSON = new ListaJSON.ListaPersona("datos-persona.json");

        Console.WriteLine("\nMostrando personas...");
        listaJSON.Mostrar();

        // ---------------------------------------------------------------------------------

        //ListaPlana.ListaPersona listaTXT = new ListaPlana.ListaPersona("datos-persona.txt");

        //Console.WriteLine("Leyendo fichero...");

        //Console.WriteLine("\nMostrando personas...");
        //listaJSON.Mostrar();

        // Agregar personas a la lista
        //Console.WriteLine("\nAñadiendo personas...");
        //l.Añadir(new Persona("Ewin", 34, 14268390, 'J'));
        //l.Guardar(); // Guardar la lista en el archivo

        //Console.WriteLine("\nListado de personas actualizado:");
        //listaTXT.Clear(); // Limpiar la lista antes de cargar desde el archivo
        //listaTXT.Listar();
        //listaTXT.Mostrar();

        //Console.WriteLine("\nQuitando personas...");
        //l.Quitar(6); // Elimina la segunda persona de la lista
        //listaTXT.Guardar(); // Guardar la lista en el archivo

        //Console.WriteLine("\nListado de personas actualizado:");
        //listaTXT.Clear(); // Limpiar la lista antes de cargar desde el archivo
        //listaTXT.Cargar();
        //listaTXT.Mostrar();

        Console.ReadKey(); // Esperar entrada del usuario para cerrar la consola
    }
}
