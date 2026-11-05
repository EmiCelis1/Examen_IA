using UnityEngine;
using System.Collections.Generic;

public class Cierre : Estado
{
    public Cierre()
    {
        this.Nombre = "Cierre";
    }

    public override void Entrar(string anterior)
    {
        Debug.Log("--- REPORTE FINAL DEL DIA ---");

        var vendidos = Ctx.ObtenerOCrear<Dictionary<string, int>>("vendidos");

        Debug.Log("Prendas vendidas:");
        foreach (KeyValuePair<string, int> kvp in vendidos)
        {
            Debug.Log($"- {kvp.Key}: {kvp.Value}");
        }

        int atendidos = Ctx.Obtener<int>("atendidos");
        int recaudado = Ctx.Obtener<int>("recaudado");
        int cancelados = Ctx.Obtener<int>("cancelados");
        int incidentes = Ctx.Obtener<int>("incidentes");

        Debug.Log($"Renglones atendidos: {atendidos}");
        Debug.Log($"Total recaudado: ${recaudado}");
        Debug.Log($"Renglones cancelados: {cancelados}");
        Debug.Log($"Incidentes (Tormentas): {incidentes}");
    }

    public override void Salir(string siguiente)
    {
    }

    public override string Actualizar(float dt)
    {
        // Estado final: no cambia automaticamente
        return null;
    }

    public override string AlRecibir(string trigger, object valor)
    {
        // Solo se sale con la tecla R desde el controller
        return null;
    }
}