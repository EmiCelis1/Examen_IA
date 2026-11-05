using UnityEngine;

public abstract class Estado
{
    public Contexto Ctx { get; set; }
    public string Nombre { get; set; }

    public abstract void Entrar(string anterior);
    public abstract void Salir(string siguiente);
    public abstract string Actualizar(float dt);
    public abstract string AlRecibir(string trigger, object valor);
}