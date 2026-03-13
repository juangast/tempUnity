using UnityEngine;
using TMPro;

public class NPCPregunta_Juan : MonoBehaviour
{
    [Header("Pregunta")]
    [TextArea(3, 6)]
    public string pregunta = "¿Cuánto es 2 + 2?";
    public string respuestaCorrecta = "4";

    [Header("Referencias")]
    public UIController_Juan uiController;
    public GameObject panelPregunta;
    public TextMeshProUGUI textoPregunta;
    public TMP_InputField inputRespuesta;
    public TextMeshProUGUI mensajeTexto;

    private bool jugadorCerca = false;
    private bool yaRespondio = false;
    private bool esteActivo = false;

    private static NPCPregunta_Juan npcActual = null;

    void Start()
    {
        panelPregunta.SetActive(false);
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && !yaRespondio && npcActual == null && Input.GetKeyDown(KeyCode.E))
        {
            AbrirPregunta();
        }

        if (esteActivo && Input.GetKeyDown(KeyCode.Return))
        {
            VerificarRespuesta();
        }

        if (esteActivo && Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarPregunta();
        }
    }

    void AbrirPregunta()
    {
        if (SFXController_Juan.Instance != null)
            SFXController_Juan.Instance.PlayMagicBook();
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
        npcActual = this;
        esteActivo = true;
        panelPregunta.SetActive(true);
        textoPregunta.text = pregunta;
        inputRespuesta.text = "";
        inputRespuesta.ActivateInputField();
        Time.timeScale = 0f;
    }

    void VerificarRespuesta()
    {
        if (inputRespuesta.text.Trim().ToLower() == respuestaCorrecta.Trim().ToLower())
        {
            uiController.AddKey(1);
            yaRespondio = true;
            CerrarPregunta();
        }
        else
        {
            textoPregunta.text = "¡Incorrecto! Intenta de nuevo:\n" + pregunta;
            inputRespuesta.text = "";
            inputRespuesta.ActivateInputField();
        }
    }

    void CerrarPregunta()
    {
        esteActivo = false;
        npcActual = null;
        panelPregunta.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (mensajeTexto != null && !yaRespondio)
            {
                mensajeTexto.text = "Presiona E para interactuar";
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
            CerrarPregunta();
        }
    }
}
