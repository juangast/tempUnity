using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class AdministradorPreguntasAaron : MonoBehaviour
{
    private enum AccionPendienteAaron { Ninguna, Red, Resortera, VaraMagica, EspadaMadera }

    public AdministradorBatallaAaron administradorBatalla;
    public BancoPreguntasAaron bancoPreguntas;
    public AdministradorConcentracionAaron administradorConcentracion;
    public SFXManagerAaron sfxManager;
    public GameObject panelObjetos, panelPreguntas;
    public TextMeshProUGUI textoPregunta, textoTipo, textoDificultad;
    public TextMeshProUGUI textoRespuestaA, textoRespuestaB, textoRespuestaC, textoRespuestaD;
    public Button botonRespuestaA, botonRespuestaB, botonRespuestaC, botonRespuestaD;
    public Image imagenVaraMagica;
    public Color colorFlashVara = Color.red;
    public float duracionFlashVara = 0.25f;
    public float esperaAntesTurnoEnemigo;
    public GameObject efectoSubida, efectoBajada;

    private Color colorOriginalVara;
    private Coroutine rutinaFlashVara;
    private AccionPendienteAaron accionPendiente = AccionPendienteAaron.Ninguna;
    private BancoPreguntasAaron.PreguntaAaron preguntaActual;

    private void Start()
    {
        if (sfxManager == null) sfxManager = FindAnyObjectByType<SFXManagerAaron>();
        if (efectoSubida == null || efectoBajada == null)
        {
            AdministradorEfectosEstadoAaron efectos = FindAnyObjectByType<AdministradorEfectosEstadoAaron>();
            if (efectos != null)
            {
                if (efectoSubida == null) efectoSubida = efectos.efectoSubida;
                if (efectoBajada == null) efectoBajada = efectos.efectoBajada;
            }
        }
        if (panelPreguntas != null) panelPreguntas.SetActive(false);
        if (panelObjetos != null) panelObjetos.SetActive(true);
        if (imagenVaraMagica != null) colorOriginalVara = imagenVaraMagica.color;
        if (botonRespuestaA != null) { botonRespuestaA.onClick.RemoveAllListeners(); botonRespuestaA.onClick.AddListener(() => ValidarRespuestaAaron(BancoPreguntasAaron.RespuestaCorrectaAaron.A)); }
        if (botonRespuestaB != null) { botonRespuestaB.onClick.RemoveAllListeners(); botonRespuestaB.onClick.AddListener(() => ValidarRespuestaAaron(BancoPreguntasAaron.RespuestaCorrectaAaron.B)); }
        if (botonRespuestaC != null) { botonRespuestaC.onClick.RemoveAllListeners(); botonRespuestaC.onClick.AddListener(() => ValidarRespuestaAaron(BancoPreguntasAaron.RespuestaCorrectaAaron.C)); }
        if (botonRespuestaD != null) { botonRespuestaD.onClick.RemoveAllListeners(); botonRespuestaD.onClick.AddListener(() => ValidarRespuestaAaron(BancoPreguntasAaron.RespuestaCorrectaAaron.D)); }
    }

    private bool PuedeActuar()
    {
        if (administradorBatalla == null) return false;
        if (administradorBatalla.EstaBatallaTerminadaAaron()) return false;
        if (!administradorBatalla.EsTurnoJugadorAaron()) return false;
        return true;
    }

    public void SeleccionarRedAaron()
    {
        SeleccionarArmaAaron(AccionPendienteAaron.Red, "Elige bien para usar Red.");
    }

    public void SeleccionarResorteraAaron()
    {
        SeleccionarArmaAaron(AccionPendienteAaron.Resortera, "Elige bien para usar Resortera.");
    }

    public void SeleccionarEspadaMaderaAaron()
    {
        SeleccionarArmaAaron(AccionPendienteAaron.EspadaMadera, "Elige bien para usar Espada.");
    }

    public void SeleccionarVaraMagicaAaron()
    {
        if (!PuedeActuar()) return;
        if (sfxManager != null) sfxManager.SonarClickAaron();
        if (!administradorBatalla.TieneUsosVaraMagicaDisponiblesAaron())
        {
            administradorBatalla.MostrarMensajeSinUsosVaraAaron();
            HacerFlashVaraMagicaAaron();
            return;
        }
        accionPendiente = AccionPendienteAaron.VaraMagica;
        AbrirPreguntaAaron("Elige bien para usar Vara.");
    }

    private void SeleccionarArmaAaron(AccionPendienteAaron accion, string mensaje)
    {
        if (!PuedeActuar()) return;
        if (sfxManager != null) sfxManager.SonarClickAaron();
        accionPendiente = accion;
        AbrirPreguntaAaron(mensaje);
    }

    private void AbrirPreguntaAaron(string mensaje)
    {
        administradorBatalla.MostrarMensajeAaron(mensaje, administradorBatalla.colorMensajeNeutral);
        if (!CargarPreguntaDesdeBancoAaron())
        {
            administradorBatalla.MostrarMensajeAaron("No hay preguntas disponibles.", administradorBatalla.colorMensajeMalo);
            return;
        }
        if (panelObjetos != null) panelObjetos.SetActive(false);
        if (panelPreguntas != null) panelPreguntas.SetActive(true);
    }

    private bool CargarPreguntaDesdeBancoAaron()
    {
        if (bancoPreguntas == null) { Debug.LogError("Falta conectar BancoPreguntasAaron en AdministradorPreguntasAaron."); return false; }
        preguntaActual = bancoPreguntas.ObtenerSiguientePreguntaAaron();
        if (preguntaActual == null) return false;
        if (textoPregunta != null) textoPregunta.text = preguntaActual.enunciado;
        if (textoTipo != null) textoTipo.text = preguntaActual.tipo;
        if (textoDificultad != null) textoDificultad.text = preguntaActual.dificultad.ToString();
        if (textoRespuestaA != null) textoRespuestaA.text = preguntaActual.opcionA;
        if (textoRespuestaB != null) textoRespuestaB.text = preguntaActual.opcionB;
        if (textoRespuestaC != null) textoRespuestaC.text = preguntaActual.opcionC;
        if (textoRespuestaD != null) textoRespuestaD.text = preguntaActual.opcionD;
        return true;
    }

    public void ValidarRespuestaAaron(BancoPreguntasAaron.RespuestaCorrectaAaron respuestaElegida)
    {
        if (preguntaActual == null) { Debug.LogError("No hay pregunta actual cargada."); return; }
        if (sfxManager != null) sfxManager.SonarClickAaron();
        bool esCorrecta = respuestaElegida == preguntaActual.respuestaCorrecta;
        if (esCorrecta)
        {
            if (administradorBatalla != null) administradorBatalla.RegistrarRespuestaCorrectaAaron();
            if (sfxManager != null) sfxManager.SonarCorrectaAaron();
            ReactivarEfectoAaron(efectoSubida);
            if (administradorConcentracion != null) administradorConcentracion.ActualizarUIConcentracionAaron();
            EjecutarAccionPendienteAaron();
        }
        else
        {
            if (administradorBatalla != null) administradorBatalla.RegistrarRespuestaIncorrectaAaron();
            if (sfxManager != null) sfxManager.SonarIncorrectaAaron();
            ReactivarEfectoAaron(efectoBajada);
            if (administradorConcentracion != null) administradorConcentracion.ActualizarUIConcentracionAaron();
            if (administradorBatalla != null) administradorBatalla.MostrarMensajeAaron("¡Fallaste!", administradorBatalla.colorMensajeMalo);
            accionPendiente = AccionPendienteAaron.Ninguna;
            preguntaActual = null;
        }
        if (panelPreguntas != null) panelPreguntas.SetActive(false);
        if (panelObjetos != null) panelObjetos.SetActive(true);
        if (administradorBatalla != null && !administradorBatalla.EstaBatallaTerminadaAaron())
            StartCoroutine(EsperarYTurnoEnemigoAaron());
    }

    private IEnumerator EsperarYTurnoEnemigoAaron()
    {
        yield return new WaitForSeconds(esperaAntesTurnoEnemigo);
        if (administradorBatalla != null && !administradorBatalla.EstaBatallaTerminadaAaron())
            administradorBatalla.IniciarTurnoEnemigoAaron();
    }

    private void EjecutarAccionPendienteAaron()
    {
        if (administradorBatalla == null) { Debug.LogError("Falta conectar administradorBatalla en AdministradorPreguntasAaron."); return; }
        switch (accionPendiente)
        {
            case AccionPendienteAaron.Red: administradorBatalla.AtacarConRed(); break;
            case AccionPendienteAaron.Resortera: administradorBatalla.AtacarConResortera(); break;
            case AccionPendienteAaron.VaraMagica: administradorBatalla.UsarVaraMagica(); break;
            case AccionPendienteAaron.EspadaMadera: administradorBatalla.AtacarConEspadaMadera(); break;
        }
        accionPendiente = AccionPendienteAaron.Ninguna;
        preguntaActual = null;
    }

    private void HacerFlashVaraMagicaAaron()
    {
        if (imagenVaraMagica == null) return;
        if (rutinaFlashVara != null) StopCoroutine(rutinaFlashVara);
        rutinaFlashVara = StartCoroutine(FlashVaraMagicaAaron());
    }

    private IEnumerator FlashVaraMagicaAaron()
    {
        imagenVaraMagica.color = colorFlashVara;
        yield return new WaitForSeconds(duracionFlashVara);
        imagenVaraMagica.color = colorOriginalVara;
        rutinaFlashVara = null;
    }

    private void ReactivarEfectoAaron(GameObject efecto)
    {
        if (efecto == null) return;
        efecto.SetActive(true);
        Animator anim = efecto.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
        }
    }
}