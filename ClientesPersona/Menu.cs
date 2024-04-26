using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesPersona;
public class Menu
{
    List<string> opciones;

    public Menu()
    {

    }
    public void Add(String opcion)
    {
        opciones.Add(opcion);
    }

    public int GetNumOpciones()
    {
        return opciones.Count();
    }

    public String GetOpcion(int n)
    {
        return (String)opciones[n];
    }

    public String ToString()
    {
        String frase = $"\n{opciones[0]}";

        for (int i = 1; i < opciones.Count; i++)
        {
            frase += $"\n{opciones[i]}";
            //frase += String.Format($"\n{opciones[i]}");
        }
        return frase;
    }

    public int Leer()
    {
        if (opciones.Count == 0)
        {
            throw new Exception("Error, el menú no tiene opciones.");
        }

        bool encontrado = false;

        int i = 0;
        int numOpcion = 0;
        while (i < 3 && !encontrado)
        {
            Console.WriteLine("Elija una opción: ");
            if (int.TryParse(Console.ReadLine(), out numOpcion))
            {
                if (numOpcion > opciones.Count)
                {
                    Console.WriteLine("La opción elegida no es válida, está fuera del rango de opciones.");
                    i++;
                }
                else
                {
                    encontrado = true;
                }
            }
        }
        return numOpcion;
    }
}
