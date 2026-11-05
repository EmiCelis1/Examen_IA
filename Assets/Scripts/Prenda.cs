using UnityEngine;

public class Prenda
{

    public int numero;
    public string nombre;
    public int costo;
    public float duracion;


    public Prenda(int numero, string nombre, int costo, float duracion)
    {
        this.numero = numero;
        this.nombre = nombre;
        this.costo = costo;
        this.duracion = duracion;
    }

    public string Describir()
    {
        return $"Prenda: {nombre}, Costo: {costo}, Duración: {duracion} segundos, Número: {numero}";
    }



}
