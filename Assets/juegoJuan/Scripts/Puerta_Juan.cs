using UnityEngine;
using TMPro;

public class Puerta_Juan : MonoBehaviour
{
    public UIController_Juan uiController;
    public TextMeshProUGUI mensajeTexto;

    private bool jugadorCerca = false;

    void Start()
    {
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (uiController.UseKey())
            {
                if (SFXController_Juan.Instance != null)
                    SFXController_Juan.Instance.PlayItem();
                if (mensajeTexto != null)
                    mensajeTexto.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (mensajeTexto != null)
            {
                if (uiController.GetKeys() > 0)
                    mensajeTexto.text = "Presiona E para abrir";
                else
                    mensajeTexto.text = "Necesitas una llave";
                mensajeTexto.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (mensajeTexto != null)
                mensajeTexto.gameObject.SetActive(false);
        }
    }
}
