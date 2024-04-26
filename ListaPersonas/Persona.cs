namespace ListaPersonas;

public class Persona
{
    public string nombre;
    public int edad;
    public int DniNumero;
    public char DniLetra;

    public int Edad
    {
        get { return edad; }
        set
        {
            if (edad < 0 || edad > 120)
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

    public Persona(string nombre, int edad, int dniNumero, char dniLetra)
    {
        Nombre = nombre;
        Edad = edad;
        DniNumero = dniNumero;
        DniNumero = dniNumero;
        DniLetra = dniLetra;
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
