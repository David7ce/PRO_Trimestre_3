namespace ListaJSON;

using ListarPersonas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class PersonaJSON
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public int DniNum { get; set; }
    public char DniLetra { get; set; }
}

public class ListaPersona : List<Persona>
{
    private string FileName;

    public ListaPersona(string fileName)
    {
        FileName = fileName;
        Cargar();
    }

    /// <summary>
    /// Carga los datos desde el archivo JSON
    /// </summary>
    private void Cargar()
    {
        //string jsonData;
        try
        {
            //using (StreamReader sr = new StreamReader(nfich))
            //{
            //    jsonData = sr.ReadToEnd();
            //}
            string jsonData = File.ReadAllText(FileName);
            //this.Clear();
            // Deserializa el JSON a una lista de PersonaJSON
            this.AddRange(JsonSerializer.Deserialize<List<Persona>>(jsonData));

            List<PersonaJSON> listaPJson = JsonSerializer.Deserialize<List<PersonaJSON>>(jsonData);

            foreach (PersonaJSON p in listaPJson)
            {
                try
                {
                    Add(new Persona(p.Nombre, p.Edad, p.DniNum, p.DniLetra));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }
    }

    /// <summary>
    /// Muestra por consola las personas de la lista
    /// </summary>
    public void Mostrar()
    {
        foreach (var persona in this)
        {
            Console.WriteLine($"Nombre: {persona.Nombre}, Edad: {persona.Edad}, DNI: {persona.DniNumero}-{persona.DniLetra}");
        }
    }

    /// <summary>
    /// Guarda la lista de personas en un archivo JSON
    /// </summary>
    public void Guardar()
    {
        try
        {
            // Serializa la lista de PersonaJSON a JSON
            string jsonData = JsonSerializer.Serialize(this);

            // Escribe el JSON en el archivo
            File.WriteAllText(FileName, jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al escribir en el archivo: {ex.Message}");
        }
    }
}
