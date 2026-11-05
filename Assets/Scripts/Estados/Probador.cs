using UnityEngine;
using System.Collections.Generic;

public class Probador : Estado
{
    public Probador()
    {
        this.Nombre = "Probador";
    }

    public override void Entrar(string anterior)
    {
        Debug.Log("No hay trabajo en curso.");
    }

    public override void Salir(string siguiente)
    {
    }

    public override string Actualizar(float dt)
    {
        return null;
    }

    public override string AlRecibir(string trigger, object valor)
    {
        if (trigger == "encargar" && valor is int id)
        {
            var catalogo = Ctx.ObtenerOCrear<Dictionary<int, Diseno>>("catalogo");

            // Guard clause 1: Validar si el ID existe en el diccionario ANTES de acceder a el
            if (catalogo == null || !catalogo.ContainsKey(id) || catalogo[id] == null)
            {
                Debug.Log("no existe ese id");
                return null;
            }

            int consecutivo = Ctx.Obtener<int>("consecutivo");
            int numero = consecutivo + 1;

            // Intentar crear la prenda (devuelve null si no hay existencias)
            Prenda r = catalogo[id].Crear(numero);

            // Guard clause 2: Validar si no hay existencias (r == null)
            if (r == null)
            {
                Debug.Log("agotado");
                return null;
            }

            // Agregar prenda a la orden y guardar nuevo consecutivo
            var orden = Ctx.ObtenerOCrear<List<Prenda>>("orden");
            orden.Add(r);
            Ctx.Fijar("consecutivo", numero);

            return "Cosiendo";
        }

        if (trigger == "cerrar")
        {
            int atendidos = Ctx.Obtener<int>("atendidos");
            if (atendidos == 0)
            {
                Debug.Log("No se han atendido renglones aun.");
                return null;
            }
            return "Cierre";
        }

        return null;
    }
}