using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTutorial_Juan : MonoBehaviour
{
    public static string escenaAnterior;

    public void IrAlMenu()
    {
        SceneManager.LoadScene("EntrarJuan");
    }

    public void Salir()
    {
        if (!string.IsNullOrEmpty(escenaAnterior))
        {
            string destino = escenaAnterior;
            escenaAnterior = null;
            SceneManager.LoadScene(destino);
        }
        else
        {
            SceneManager.LoadScene("EntrarJuan");
        }
    }
}
