using UnityEngine;
using UnityEngine.SceneManagement;

public class AdministradorPausaAaron : MonoBehaviour
{
    public GameObject panelPausa;

    public void AbrirPausaAaron()
    {
        if (panelPausa != null) panelPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CerrarPausaAaron()
    {
        if (panelPausa != null) panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void VolverAlMenuAaron()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuAaron");
    }
}