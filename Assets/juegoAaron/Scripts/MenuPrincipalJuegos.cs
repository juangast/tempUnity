using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalJuegos : MonoBehaviour
{
    public void IrAJuego1()
    {
        SceneManager.LoadScene("MenuAaron");
    }

    public void IrAJuego2()
    {
        Debug.Log("Juego 2 aún no configurado");
    }

    public void IrAJuego3()
    {
        Debug.Log("Juego 3 aún no configurado");
    }
}
