using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFinal_Juan : MonoBehaviour
{
    public void Reiniciar()
    {
        SceneManager.LoadScene("JuegoJuan");
    }

    public void SalirAlMenu()
    {
        SceneManager.LoadScene("EntrarJuan");
    }
}
