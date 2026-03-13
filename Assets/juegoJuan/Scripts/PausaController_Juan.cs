using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaController_Juan : MonoBehaviour
{
    private bool enPausa = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !enPausa)
        {
            Pausar();
        }
    }

    public void Pausar()
    {
        if (enPausa) return;
        enPausa = true;
        Time.timeScale = 0f;
        SceneManager.LoadSceneAsync("MenuJuan", LoadSceneMode.Additive);
    }

    public void Reanudar()
    {
        enPausa = false;
    }
}
