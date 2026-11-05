using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class SastreriaController : MonoBehaviour
{
    private Maquina maquina;
    private Contexto ctx;
    private string ultimo;

    public void Start()
    {
        Debug.Log("Inicio del programa");
        Debug.Log("1, 2, 3: Encargar id 1, 2, 3");
        Debug.Log("4: Encargar id 4 (inexistente)");
        Debug.Log("P: Pagar 5000 | L: Pagar 50 | Z: Pagar 0");
        Debug.Log("C: Cancelar ultimo renglon");
        Debug.Log("I: Simular Tormenta");
        Debug.Log("X: Cerrar el dia");
        Debug.Log("R: Reiniciar todo");

        ctx = new Contexto();
        
        CargarCatalogo();
        
        List<Estado> estados = new List<Estado>
        {
            new Probador(),
            new Cosiendo(),
            new Caja(),
            new Tormenta()
        };

        foreach (Estado estado in estados)
        {
            estado.Ctx = ctx;
        }

        maquina = new Maquina(estados, ctx);
       
        maquina.Iniciar("Probador");
        ultimo = maquina.Actual != null ? maquina.Actual.Nombre : "";
        Debug.Log($"Estado actual: {ultimo}");



    }

    public void Update()
    {
        if (Keyboard.current == null || maquina == null || maquina.Actual == null)
        {
            return;
        }

        var kb = Keyboard.current;

        if (kb.digit1Key.wasPressedThisFrame) maquina.Disparar("encargar", 1);
        else if (kb.digit2Key.wasPressedThisFrame) maquina.Disparar("encargar", 2);
        else if (kb.digit3Key.wasPressedThisFrame) maquina.Disparar("encargar", 3);
        else if (kb.digit4Key.wasPressedThisFrame) maquina.Disparar("encargar", 4);
        else if (kb.pKey.wasPressedThisFrame) maquina.Disparar("pagar", 5000);
        else if (kb.lKey.wasPressedThisFrame) maquina.Disparar("pagar", 50);
        else if (kb.zKey.wasPressedThisFrame) maquina.Disparar("pagar", 0);
        else if (kb.cKey.wasPressedThisFrame) maquina.Disparar("cancelar", 0);
        else if (kb.iKey.wasPressedThisFrame) maquina.Disparar("tormenta", 0);
        else if (kb.xKey.wasPressedThisFrame) maquina.Disparar("cerrar", 0);
        else if (kb.rKey.wasPressedThisFrame)
        {
            maquina.Ctx.Limpiar();
            CargarCatalogo();
            maquina.CambiarA("Probador");
        }

        maquina.Actualizar(Time.deltaTime);

        if (maquina.Actual != null && maquina.Actual.Nombre != ultimo)
        {
            ultimo = maquina.Actual.Nombre;
            Debug.Log($"Estado actual: {ultimo}");
        }
    }

    private void CargarCatalogo()
    {
        var catalogo = ctx.ObtenerOCrear<Dictionary<int, Diseno>>("catalogo");
        catalogo.Clear();

        
        catalogo[1] = new Diseno("Camiseta", 280, 6.0f, 1);
        catalogo[2] = new Diseno("Vestido", 700, 9.0f, 3);
        catalogo[3] = new Diseno("Sombrero", 350, 8.0f, 2);
        
    }
}
