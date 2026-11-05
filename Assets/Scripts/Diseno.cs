using UnityEngine;

public class Diseno
{
    public string nombre;
    public int costo;
    public float duracion;
    public int existencias;

    public Diseno(string nombre, int costo, float duracion, int existencias)
    {
        this.nombre = nombre;
        this.costo = costo;
        this.duracion = duracion;
        this.existencias = existencias;
    }

    public Prenda Crear(int numero)
    {
        if (existencias <= 0)
        {
            Debug.Log($"No hay existencias disponibles para el diseño: {nombre}");
            return null;
        }

        existencias--;
        return new Prenda(numero, nombre, costo, duracion);
    }

}
