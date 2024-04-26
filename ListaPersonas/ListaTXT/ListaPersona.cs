using ListaPersonas.ListaJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaPersonas.ListaTXT;

public class ListaPersona : List<Persona>
{
    private string FileName;

    public ListaPersona(string fileName)
    {
        FileName = fileName;
        Cargar();
    }

    /// <summary>
    /// Carga o lista los archivos leyéndolos del arhivo de texto
    /// </summary>
    private void Cargar()
    {
        try
        {
            using (StreamReader sr = new StreamReader(FileName))
            {
                string nombre;
                while ((nombre = sr.ReadLine()) != null) // while (!sr.EndOfStream)
                {
                    int edad = Convert.ToInt32(sr.ReadLine());
                    int dniNumero = Convert.ToInt32(sr.ReadLine());
                    char dniLetra = Convert.ToChar(sr.ReadLine());

                    // Crear una nueva instancia de Persona utilizando el constructor de la clase base
                    try
                    {
                        Persona persona = new Persona(nombre, edad, dniNumero, dniLetra);
                        this.Add(persona); // Agregar la persona a la lista de personas
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al crear persona: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }
    }

    /// <summary>
    /// Muestra por consola las personas de la lista leída, la función que Fernando llama Listar
    /// </summary>
    public void Mostrar()
    {
        foreach (var persona in this)
            Console.WriteLine(persona);
    }

    public void Guardar()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                foreach (Persona persona in this)
                {
                    sw.WriteLine(persona.Nombre);
                    sw.WriteLine(persona.Edad);
                    sw.WriteLine(persona.DniNumero);
                    sw.WriteLine(persona.DniLetra);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al escribir en el archivo: {ex.Message}");
        }
    }

    /// <summary>
    /// Añade una nueva persona a la lista
    /// </summary>
    /// <param name="persona"></param>
    public void Añadir(Persona persona)
    {
        this.Add(persona);
        Console.WriteLine($"Se añadió a {persona.Nombre} a la lista.");
    }

    /// <summary>
    /// Elimina una persona de la lista
    /// </summary>
    /// <param name="indice"></param>
    public void Quitar(int indice)
    {
        // Convertir el índice 1-based a 0-based
        int indexToRemove = indice - 1;

        if (indexToRemove >= 0 && indexToRemove < this.Count)
        {
            Persona personaEliminada = this[indexToRemove];
            this.RemoveAt(indexToRemove);
            Console.WriteLine($"Se quitó a {personaEliminada.Nombre} de la lista.");
        }
        else
        {
            Console.WriteLine("Índice fuera de rango. No se pudo quitar la persona.");
        }
    }

    public override string ToString()
    {
        int i;
        String s = "{ ";

        for (i = 0; i < (Count - 1); i++)
            s = s + $"{this.ElementAt(i)} , ";
        if (Count > 0)
            s = s + this.ElementAt(i);

        s = s + " }";

        Console.WriteLine(" }");

        return s;
    }
}
