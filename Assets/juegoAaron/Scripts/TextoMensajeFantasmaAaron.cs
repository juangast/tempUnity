using UnityEngine;
using TMPro;
using System.Collections;

public class TextoMensajeFantasmaAaron : MonoBehaviour
{
    public GameObject panelMensaje;
    public CanvasGroup canvasGroupPanel;
    public TextMeshProUGUI texto;
    public float duracion = 3.0f;
    public float desplazamientoY = 6f;

    private RectTransform rectTransformTexto;
    private Vector2 posicionInicialTexto;
    private Coroutine rutinaActual;

    private void Awake()
    {
        if (texto == null) texto = GetComponentInChildren<TextMeshProUGUI>(true);
        if (canvasGroupPanel == null && panelMensaje != null) canvasGroupPanel = panelMensaje.GetComponent<CanvasGroup>();
        if (texto != null)
        {
            rectTransformTexto = texto.GetComponent<RectTransform>();
            posicionInicialTexto = rectTransformTexto.anchoredPosition;
            texto.text = "";
        }
        if (canvasGroupPanel != null) canvasGroupPanel.alpha = 0f;
        if (panelMensaje != null) panelMensaje.SetActive(false);
    }

    public void MostrarMensajeAaron(string contenido)
    {
        MostrarMensajeAaron(contenido, Color.white);
    }

    public void MostrarMensajeAaron(string contenido, Color colorBase)
    {
        if (texto == null || panelMensaje == null || canvasGroupPanel == null) return;
        if (rutinaActual != null) StopCoroutine(rutinaActual);
        rutinaActual = StartCoroutine(EfectoMensajeAaron(contenido, colorBase));
    }

    private IEnumerator EfectoMensajeAaron(string contenido, Color colorBase)
    {
        panelMensaje.SetActive(true);
        canvasGroupPanel.alpha = 1f;
        texto.text = contenido;
        texto.color = colorBase;
        rectTransformTexto.anchoredPosition = posicionInicialTexto;
        Vector2 posicionFinal = posicionInicialTexto + new Vector2(0f, desplazamientoY);
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;
            rectTransformTexto.anchoredPosition = Vector2.Lerp(posicionInicialTexto, posicionFinal, t);
            if (t < 0.78f)
                canvasGroupPanel.alpha = 1f;
            else
                canvasGroupPanel.alpha = Mathf.Lerp(1f, 0f, (t - 0.78f) / 0.22f);
            yield return null;
        }
        rectTransformTexto.anchoredPosition = posicionInicialTexto;
        canvasGroupPanel.alpha = 0f;
        texto.text = "";
        panelMensaje.SetActive(false);
        rutinaActual = null;
    }
}