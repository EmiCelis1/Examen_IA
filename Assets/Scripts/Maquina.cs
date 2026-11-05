using System.Collections.Generic;
using UnityEngine;

public class Maquina
{
    public string trigger, destino;
    public Contexto Ctx { get; set; }
    public Dictionary<string, Estado> estados = new Dictionary<string, Estado>();
    public Estado Actual { get; set; }
    public string Anterior { get; set; }
    public string Siguiente { get; set; }

    public Maquina(List<Estado> lista, Contexto ctx)
    {
        Ctx = ctx; 

        foreach (var estado in lista)
        {
            estados[estado.Nombre] = estado;
        }
    }

    public void Iniciar(string nombre)
    {
        CambiarA(nombre);
    }

    public void CambiarA(string nombre)
    {
        if (!estados.ContainsKey(nombre))
        {
            Debug.LogWarning($"No hay estado registrado con el nombre: {nombre}");
            return;
        }

        Siguiente = nombre;
        Anterior = null;

        if (Actual != null)
        {
            Anterior = Actual.Nombre;
            Actual.Salir(Siguiente);
        }

        Actual = estados[nombre]; 
        Actual.Entrar(Anterior);
    }

    public void Disparar(string trigger, int valor = 0)
    {
        if (Actual != null)
        {
            this.trigger = trigger;
            destino = Actual.AlRecibir(trigger, valor);

            if (!string.IsNullOrEmpty(destino) && destino != Actual.Nombre)
            {
                CambiarA(destino);
            }
        }
    }

    public string Actualizar(float dt)
    {
        if (Actual == null) return null; 

        destino = Actual.Actualizar(dt);

        if (!string.IsNullOrEmpty(destino) && destino != Actual.Nombre)
        {
            CambiarA(destino);
        }

        return destino;
    }
}