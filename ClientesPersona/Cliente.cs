using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesPersona;
public class Cliente : Persona
{
    List<Cliente> clientes = new List<Cliente>();

    public Cliente(int n, char l) : base(n, l)
    {
        clientes.Add(this);
    }

    public void CrearCliente(string nombre, int edad, int dni, char letra)
    {
        // Crear una nueva instancia de Cliente con los parámetros proporcionados
        Cliente nuevoCliente = new Cliente(dni, letra);

        // Asignar el nombre y la edad al nuevo cliente
        nuevoCliente.Nombre = nombre;
        nuevoCliente.Edad = edad;

        // Agregar el nuevo cliente a la lista de clientes
        clientes.Add(nuevoCliente);
    }

    public Cliente BuscarCliente(int dni, char letra)
    {
        return clientes.Find(c => c.DniNumero == dni && c.DniLetra == letra);
    }

    public string MostrarCliente(int dni, char letra)
    {
        Cliente cliente = BuscarCliente(dni, letra);

        if (cliente == null)
        {
            throw new Exception("Cliente no encontrado");
        }

        return cliente.ToString();

    }

    public void EliminarCliente()
    {
        clientes.Remove(this);
    }

    public void Finalizar()
    {
        Console.WriteLine("Programa finalizado");
    }
}
