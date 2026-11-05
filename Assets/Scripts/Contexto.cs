using System.Collections.Generic;

public class Contexto
{  
    private Dictionary<string, object> datos = new Dictionary<string, object>();

    public void Fijar<T>(string clave, T valor)
    {
        datos[clave] = valor;
    }

    public T Obtener<T>(string clave)
    {
        if (datos.TryGetValue(clave, out object valor) && valor is T valorConvertido)
        {
            return valorConvertido;
        }
        return default;
    }

    public T ObtenerOCrear<T>(string clave) where T : new()
    {
        if (datos.TryGetValue(clave, out object valor) && valor is T valorConvertido)
        {
            return valorConvertido;
        }

        T nuevo = new T();
        datos[clave] = nuevo;
        return nuevo;
    }

    public void Limpiar()
    {
        datos.Clear();
    }
}