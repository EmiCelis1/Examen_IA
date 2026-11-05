using UnityEngine;
using System.Collections.Generic;

public class Caja : Estado
{
    // Campo propio segun la especificacion del Estado 3
    private int total;

    public Caja()
    {
        this.Nombre = "Caja";
    }

    public override void Entrar(string anterior)
    {
        total = 0;
        var orden = Ctx.ObtenerOCrear<List<Prenda>>("orden");

        // Recorre la orden para sumar costos e imprimir la descripcion de cada renglon
        foreach (Prenda r in orden)
        {
            total += r.costo;
            Debug.Log(r.Describir());
        }

        Debug.Log($"Total a pagar: ${total}");
        Ctx.Fijar("total", total);
    }

    public override void Salir(string siguiente)
    {
    }

    public override string Actualizar(float dt)
    {
        // Caja no realiza transiciones por tiempo
        return null;
    }

    public override string AlRecibir(string trigger, object valor)
    {
        // Flujo del evento "pagar" segun la Figura 3 (Seccion 7.3)
        if (trigger == "pagar" && valor is int pago)
        {
            // Guard clause 1: ¿valor <= 0?
            if (pago <= 0)
            {
                Debug.Log("pago no valido");
                return null;
            }

            // Guard clause 2: ¿valor < total?
            if (pago < total)
            {
                int faltan = total - pago;
                Debug.Log($"faltan {faltan}");
                return null;
            }

            // Procesar cobro exitoso
            var vendidos = Ctx.ObtenerOCrear<Dictionary<string, int>>("vendidos");
            var orden = Ctx.ObtenerOCrear<List<Prenda>>("orden");

            // Foreach sobre la orden para acumular unidades vendidas por nombre
            foreach (Prenda r in orden)
            {
                if (!vendidos.ContainsKey(r.nombre))
                {
                    vendidos[r.nombre] = 0;
                }
                vendidos[r.nombre]++;
            }

            // Actualizar acumulados globales en el Contexto
            int recaudadoActual = Ctx.Obtener<int>("recaudado");
            Ctx.Fijar("recaudado", recaudadoActual + total);

            int atendidosActual = Ctx.Obtener<int>("atendidos");
            Ctx.Fijar("atendidos", atendidosActual + orden.Count);

            // Vaciar la orden de la compra actual
            orden.Clear();

            // Informar cambio y retornar a Probador
            int cambio = pago - total;
            Debug.Log($"cambio = {cambio}");

            return "Probador";
        }

        // Evento tormenta
        if (trigger == "tormenta")
        {
            return "Tormenta";
        }

        return null;
    }
}