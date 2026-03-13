using UnityEngine;
using UnityEngine.UI;

public class ControlVisualVidaAaron : MonoBehaviour
{
    public EstadosPersonajeAaron personaje;
    public Image imagenVida;
    public Sprite vida10, vida30, vida45, vida65, vida80, vida100;
    public Sprite enemigo0, enemigo33, enemigo77, enemigo100;
    public bool esBarraJugador = true;

    public void ActualizarImagenVida()
    {
        float p = personaje.ObtenerPorcentajeVida();
        if (esBarraJugador)
        {
            if (p <= 10) imagenVida.sprite = vida10;
            else if (p <= 30) imagenVida.sprite = vida30;
            else if (p <= 45) imagenVida.sprite = vida45;
            else if (p <= 65) imagenVida.sprite = vida65;
            else if (p <= 80) imagenVida.sprite = vida80;
            else imagenVida.sprite = vida100;
        }
        else
        {
            if (p <= 0) imagenVida.sprite = enemigo0;
            else if (p <= 33) imagenVida.sprite = enemigo33;
            else if (p <= 77) imagenVida.sprite = enemigo77;
            else imagenVida.sprite = enemigo100;
        }
    }
}