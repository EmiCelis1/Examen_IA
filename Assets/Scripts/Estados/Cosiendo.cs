using UnityEngine;
using System.Collections.Generic;

public class Cosiendo : Estado
{
    
    private float tiempo;
    private float espera;

    public Cosiendo()
    {
        this.Nombre = "Cosiendo";
    }

    public override void Entrar(string anterior)
    {
        tiempo = 0f;
        espera = 0f;

        var orden = Ctx.ObtenerOCrear<List<Prenda>>("orden");

        foreach (Prenda p in orden)
        {
            espera += p.duracion;
        }

        Debug.Log($"Trabajando en la orden. Tiempo total de espera: {espera}s");
    }

    public override void Salir(string siguiente)
    {
    }

    public override string Actualizar(float dt)
    {
        var orden = Ctx.ObtenerOCrear<List<Prenda>>("orden");

        if (orden.Count == 0)
        {
            return "Probador";
        }

        tiempo += dt;

        if (tiempo >= espera)
        {
            return "Caja";
        }

        return null;
    }

    public override string AlRecibir(string trigger, object valor)
    {
        if (trigger == "encargar" && valor is int id)
        {
            var catalogo = Ctx.ObtenerOCrear<Dictionary<int, Diseno>>("catalogo");

            if (!catalogo.ContainsKey(id))
            {
                Debug.Log("no existe ese id");
                return null;
            }

            int numero = Ctx.Obtener<int>("consecutivo") + 1;
            Prenda r = catalogo[id].Crear(numero);

            // Guard clause 2
            if (r == null)
            {
                Debug.Log("agotado");
                return null;
            }

            var orden = Ctx.ObtenerOCrear<List<Prenda>>("orden");
            orden.Add(r);
            Ctx.Fijar("consecutivo", numero);

            espera += r.duracion;
            Debug.Log($"Agregado a la orden en curso: {r.Describir()}. Nueva espera: {espera}s");

            return null;
        }

        if (trigger == "cancelar")
        {
            var orden = Ctx.ObtenerOCrear<List<Prenda>>("orden");

            if (orden.Count == 0)
            {
                return "Probador";
            }

            int ultimoIndice = orden.Count - 1;
            Prenda prendaRemovida = orden[ultimoIndice];
            orden.RemoveAt(ultimoIndice);

            espera -= prendaRemovida.duracion;

            int cancelados = Ctx.Obtener<int>("cancelados") + 1;
            Ctx.Fijar("cancelados", cancelados);

            Debug.Log($"Renglon cancelado: {prendaRemovida.Describir()}");

            if (orden.Count == 0)
            {
                return "Probador";
            }

            return null;
        }

        if (trigger == "tormenta")
        {
            return "Tormenta";
        }

        return null;
    }
}