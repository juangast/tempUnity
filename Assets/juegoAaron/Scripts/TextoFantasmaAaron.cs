using UnityEngine;
using TMPro;
using System.Collections;

public class TextoFantasmaAaron : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float duracion = 1.2f;
    public float desplazamientoY = 30f;
    public Color colorDanio = Color.red;
    public Color colorCuracion = Color.green;
    public Color colorNeutral = Color.white;

    private RectTransform rectTransform;
    private Vector2 posicionInicial;
    private Coroutine rutinaActual;

    private void Awake()
    {
        if (texto == null) texto = GetComponent<TextMeshProUGUI>();
        if (texto == null) return;
        rectTransform = texto.GetComponent<RectTransform>();
        posicionInicial = rectTransform.anchoredPosition;
        Color c = texto.color;
        c.a = 0f;
        texto.color = c;
        texto.text = "";
    }

    public void MostrarTextoAaron(string contenido)
    {
        MostrarTextoAaron(contenido, colorNeutral);
    }

    public void MostrarTextoAaron(string contenido, Color colorBase)
    {
        if (texto == null || rectTransform == null) return;
        if (rutinaActual != null) StopCoroutine(rutinaActual);
        rutinaActual = StartCoroutine(EfectoFantasmaAaron(contenido, colorBase));
    }

    private IEnumerator EfectoFantasmaAaron(string contenido, Color colorBase)
    {
        texto.text = contenido;
        rectTransform.anchoredPosition = posicionInicial;
        Color color = colorBase;
        color.a = 1f;
        texto.color = color;
        Vector2 posicionFinal = posicionInicial + new Vector2(0f, desplazamientoY);
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;
            rectTransform.anchoredPosition = Vector2.Lerp(posicionInicial, posicionFinal, t);
            Color nuevoColor = colorBase;
            nuevoColor.a = Mathf.Lerp(1f, 0f, t);
            texto.color = nuevoColor;
            yield return null;
        }
        rectTransform.anchoredPosition = posicionInicial;
        Color colorFinal = colorBase;
        colorFinal.a = 0f;
        texto.color = colorFinal;
        texto.text = "";
        rutinaActual = null;
    }
}