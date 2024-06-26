﻿namespace JuegoNaves;
using System;

public class Naves
{
    // Atributos
    private int _tamaño;
    private int _vida;
    private int _limiteVida;
    private int _disparo;
    private int _escudo;

    // Propiedades para acceder a los atributos privados
    public int Tamaño { get { return _tamaño; } }
    public int Vida { get { return _vida; } }
    public int LimiteVida { get { return _limiteVida; } }
    public int Disparo { get { return _disparo; } }
    public int Escudo { get { return _escudo; } }

    // Constructor
    public Naves()
    {
        _tamaño = 1;
        _vida = 100;
        _limiteVida = 100;
        _disparo = 1;
        _escudo = 0;
    }

    // Métodos públicos
    public void AumentarVida(int cantidad)
    {
        _vida += cantidad;
        if (_vida > _limiteVida)
            _vida = _limiteVida;
    }

    public void MejorarEscudo()
    {
        if (_escudo < 10)
            _escudo++;
    }

    public void MejorarNave()
    {
        if (_tamaño < 10)
        {
            _tamaño++;
            _limiteVida += 100;
            if (_tamaño == 10)
                _limiteVida = 1000;
        }
    }

    public void MejorarDisparo()
    {
        if (_disparo < 10)
            _disparo++;
    }

    public void Atacar(int cantidad)
    {
        int ataqueRestante = cantidad;

        // Reducir escudo
        while (_escudo > 0 && ataqueRestante > 0)
        {
            _escudo--;
            ataqueRestante -= 10;
        }

        // Reducir vida
        if (ataqueRestante > 0)
        {
            _vida -= ataqueRestante;
            if (_vida <= 0)
                Console.WriteLine("La nave ha sido destruida.");
        }
    }
}
