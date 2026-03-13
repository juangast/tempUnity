using UnityEngine;

public class AdministradorEfectosEstadoAaron : MonoBehaviour
{
    public GameObject efectoSubida;
    public GameObject efectoBajada;

    public void MostrarSubidaAaron()
    {
        if (efectoSubida == null) return;
        efectoSubida.SetActive(false);
        efectoSubida.SetActive(true);
    }

    public void MostrarBajadaAaron()
    {
        if (efectoBajada == null) return;
        efectoBajada.SetActive(false);
        efectoBajada.SetActive(true);
    }
}