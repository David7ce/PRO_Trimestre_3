using System;

namespace ListaMercancias;

public class Program
{
    public static void Main(string[] args)
    {
        Decimal precioKilo;

        //Fases.B_EscribirConsola(Fases.A_CargarDatosConsola(out precioKilo), precioKilo); // arreglar este
        //Fases.B_EscribirHtml_Block(Fases.A_CargarDatosTxt(out precioKilo), precioKilo);
        //Fases.B_EscribirHtml_Format(Fases.A_CargarDatosTxt(out precioKilo), precioKilo);

        //Fases.B_EscribirJson(Fases.A_CargarDatosTxt(out precioKilo), precioKilo);
        Fases.B_EscribirBinario(Fases.A_CargarDatosTxt(out precioKilo), precioKilo);
    }
}