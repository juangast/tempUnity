using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa_Juan : MonoBehaviour
{
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("JuegoJuan");
    }

public void SalirAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("EntrarJuan");
    }
}
