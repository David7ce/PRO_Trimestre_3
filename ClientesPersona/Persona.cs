using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesPersona;
public class Persona
{
    // Atributos
    protected char DniLetra;
    protected int DniNumero;
    protected string nombre = "Desconocido";
    protected int edad;

    // Propiedades

    public int Edad
    {
        get { return edad; }
        set
        {
            if (edad < 0 && edad > 120)
            {
                throw new Exception("¡Edad no válida!");
            }
            edad = value;
        }
    }

    public string Nombre
    {
        get { return nombre; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("¡Nombre no válido!");
            }
            nombre = value;
        }
    }

    // Constructor
    public Persona(int n, char l, String nom, int edad) : this(n, l)
    {
        Nombre = nom;
        Edad = edad;
    }

    // Constructor
    public Persona(int num, char letra)
    {
        if (num > 99999999 || num < 0)
            throw new ArgumentOutOfRangeException(nameof(num), "Número de DNI no válido");

        if (letra != LetraDNI(num))
            throw new Exception("La letra del DNI no es correcta");

        DniNumero = num;
        DniLetra = letra;
    }

    public override bool Equals(object? obj)
    {
        bool iguales;
        // Con programación estructurada habría que definir un booleano
        if (obj == null || obj is not Persona) //  GetType() != obj.GetType()
            iguales = false;  // return false;
        // return this.DniNumero == otraPersona.DniNumero && this.DniLetra == otraPersona.DniLetra;
        else
        {
            Persona otraPersona = (Persona)obj;
            iguales = (this.DniNumero == otraPersona.DniNumero);
        }
        return iguales;
    }

    public char LetraDNI(int dni)
    {
        if (dni > 99999999 || dni < 0)
            throw new ArgumentOutOfRangeException(nameof(dni), "Número de DNI no válido");

        string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
        return letras[dni % 23];
    }

    public override string ToString()
    {
        return $"{Nombre} con {Edad} años tiene como DNI {DniNumero} {DniLetra}";
    }
}
