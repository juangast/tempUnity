using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal_Juan : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("JuegoJuan");
    }

    public void Tutorial()
    {
        MenuTutorial_Juan.escenaAnterior = "EntrarJuan";
        SceneManager.LoadScene("TutorialJuan");
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
