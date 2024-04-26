namespace ClientesPersona;

internal class Program
{
    static void Main(string[] args)
    {
        UnaPersona();

        /*
        Console.WriteLine(NumeroTextual(10));
        Console.WriteLine(NumeroTextual(10));

        Console.WriteLine(NumeroTextual(11));

        Console.WriteLine(NumeroTextual(20));
        Console.WriteLine(NumeroTextual(100));

        Console.WriteLine(NumeroTextual(102));
        Console.WriteLine(NumeroTextual(112));
        Console.WriteLine(NumeroTextual(122));
        Console.WriteLine(NumeroTextual(182));

        Console.WriteLine(NumeroRomano(504));
        */
    }

    private static void UnaPersona()
    {
        Persona p_A = new Persona(12345678, 'Z', "Adela", 27);
        Persona p_B = new Persona(12345678, 'Z', "Manuel", 27);

        Console.WriteLine(p_A);
        Console.WriteLine(p_B);

        if (p_A.Equals(p_B))
            Console.WriteLine("Son iguales");
        else
            Console.WriteLine("No son iguales");
    }

    // retorne un String con el número en forma textual
    public static string NumeroTextual(int num)
    {
        if (num < 1 || num > 999)
            throw new ArgumentOutOfRangeException(nameof(num), "Error, el número debe estar entre 1 y 999.");

        string numero = "";
        int centena = num / 100;
        int decena = (num / 10) % 10;
        int unidad = num % 10;

        string[] unidades = { "cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve" };
        string[] teens = { "", "once", "doce", "trece", "catorce", "quince", "dieciseis", "diecisiete", "dieciocho", "diecinueve" };
        string[] decenas = { "", "diez", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
        string[] decenasCombinada = { "", "dieci", "veinti", "treinta y", "cuarenta y", "cincuenta y", "sesenta y", "setenta y", "ochenta y", "noventa y" };
        string[] centenas = { "", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos" };

        if (centena > 0)
        {
            if (num == 100)
            {
                numero = "cien";
            }
            else
            {
                numero = centenas[centena];
                if (num % 100 != 0) // con resto de cien, es decir, con decenas o unidades
                    numero += " ";
            }
        }

        if (decena > 0)
        {
            if (decena == 1 && num % 10 != 0)
            {
                numero += teens[num % 10];
                return numero;
            }
            if (num % 10 == 0)
            {
                numero += decenas[decena];
            }
            else
            {
                numero += decenasCombinada[decena];
                if (num % 10 != 0)
                    numero += " ";
            }
        }

        if (unidad > 0)
            numero += unidades[unidad];
        else if (num == 0)
            numero = unidades[unidad];

        return numero;
    }

    public static string NumeroRomano(int num)
    {
        if (num < 1 || num > 999)
            throw new ArgumentOutOfRangeException(nameof(num), "Error, el número debe estar entre 1 y 999.");

        string[] letrasR = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        int[] numR = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        string romano = "";
        int i = 0;

        // Mientras el número restante sea positivo
        while (num > 0)
        {
            while (num >= numR[i])
            {
                romano += letrasR[i];
                num -= numR[i];
            }
            i++;
        }

        return romano;
    }

    /*
    // Diccionario para las conversiones
    private static Dictionary<char, int> valoresRomanos = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

    // Método para convertir de número decimal a romano
    public static string ConvertirAROmano(int numero)
    {
        if (numero < 0 || numero > 999)
        {
            throw new ArgumentOutOfRangeException("El número debe estar entre 0 y 999.");
        }

        string romano = "";
        foreach (var valor in valoresRomanos)
        {
            while (numero >= valor.Value)
            {
                romano += valor.Key;
                numero -= valor.Value;
            }
        }

        return romano;
    }
    */
}

