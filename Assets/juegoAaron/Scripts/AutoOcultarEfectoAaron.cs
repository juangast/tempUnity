using UnityEngine;

public class AutoOcultarEfectoAaron : MonoBehaviour
{
    public float tiempoOcultar = 0.9f;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(OcultarAaron), tiempoOcultar);
    }

    private void OcultarAaron()
    {
        gameObject.SetActive(false);
    }
}