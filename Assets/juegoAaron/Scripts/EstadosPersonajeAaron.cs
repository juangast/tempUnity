using UnityEngine;

public class EstadosPersonajeAaron : MonoBehaviour
{
    public string nombrePersonaje;
    public int vidaMaxima = 100;
    public int vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMaxima) vidaActual = vidaMaxima;
    }

    public float ObtenerPorcentajeVida()
    {
        return (float)vidaActual / vidaMaxima * 100f;
    }

    public bool EstaDerrotado()
    {
        return vidaActual <= 0;
    }
}