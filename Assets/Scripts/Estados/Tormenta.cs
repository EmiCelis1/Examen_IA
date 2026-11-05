using UnityEngine;

public class Tormenta : Estado
{
    private string estadoPrevio;
    private float tiempo;

    public Tormenta()
    {
        this.Nombre = "Tormenta";
    }

    public override void Entrar(string anterior)
    {
        estadoPrevio = anterior;
        tiempo = 0f;

        // Suma 1 a los incidentes en el Contexto
        int incidentesActuales = Ctx.Obtener<int>("incidentes") + 1;
        Ctx.Fijar("incidentes", incidentesActuales);

        Debug.Log("¡Cae una tormenta y hay que guardar las telas!");
    }

    public override void Salir(string siguiente)
    {
        Debug.Log("El incidente de la tormenta se resolvio.");
    }

    public override string Actualizar(float dt)
    {
        tiempo += dt;

        // A los 4 segundos regresa al estado donde estaba antes del incidente
        if (tiempo >= 4.0f)
        {
            return estadoPrevio;
        }

        return null;
    }

    public override string AlRecibir(string trigger, object valor)
    {
        // Ignora cualquier input durante la tormenta
        return null;
    }
}