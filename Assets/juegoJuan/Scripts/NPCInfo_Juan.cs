using UnityEngine;
using TMPro;

public class NPCInfo_Juan : MonoBehaviour
{
    [TextArea]
    public string mensaje = "Escribe tu mensaje aquí";
    public TextMeshProUGUI mensajeTexto;
    public GameObject panelFondo;

    private bool jugadorCerca = false;
    private bool mostrandoInfo = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (mostrandoInfo)
                Cerrar();
            else
                Abrir();
        }
    }

    void Start()
    {
        if (panelFondo != null)
            panelFondo.SetActive(false);
    }

    void Abrir()
    {
        if (SFXController_Juan.Instance != null)
            SFXController_Juan.Instance.PlayMagicBook();
        mostrandoInfo = true;
        if (panelFondo != null)
            panelFondo.SetActive(true);
        if (mensajeTexto != null)
        {
            mensajeTexto.text = mensaje;
            mensajeTexto.gameObject.SetActive(true);
        }
    }

    void Cerrar()
    {
        mostrandoInfo = false;
        if (panelFondo != null)
            panelFondo.SetActive(false);
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (mensajeTexto != null && !mostrandoInfo)
            {
                mensajeTexto.text = "Presiona E para hablar";
                mensajeTexto.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            Cerrar();
        }
    }
}
