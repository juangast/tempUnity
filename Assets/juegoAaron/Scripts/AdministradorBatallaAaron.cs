using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class AdministradorBatallaAaron : MonoBehaviour
{
    public EstadosPersonajeAaron jugador;
    public EstadosPersonajeAaron enemigo;
    public TextMeshProUGUI textoPorcentajeJugador, textoPorcentajeEnemigo, textoMensaje, textoATQ, textoPenalizacion;
    public GameObject impacto;
    public RectTransform puntoImpactoJugador, puntoImpactoEnemigo;
    public Animator animatorJugador, animatorEnemigo;
    public Transform visualJugador, visualEnemigo;
    public SFXManagerAaron sfxManager;
    public float tiempoAnimacionAtaqueJugador, tiempoAnimacionAtaqueEnemigo;
    public float tiempoDespuesAccionEnemigo;
    public float tiempoAntesAnimacionFinal, tiempoMuerteEnemigo;
    public float velocidadHuidaJugador, distanciaHuidaJugador, tiempoDespuesHuidaJugador;
    public TextoMensajeFantasmaAaron feedbackMensaje;
    public Color colorMensajeNeutral = Color.white;
    public Color colorMensajeBueno = Color.green;
    public Color colorMensajeMalo = Color.red;
    public TextoFantasmaAaron feedbackVidaJugador, feedbackVidaEnemigo;
    public ControlVisualVidaAaron controlVidaJugador, controlVidaEnemigo;
    public AdministradorEnemigoAaron administradorEnemigo;
    public bool esTurnoJugador = true;
    public float tiempoEsperaTurnoEnemigo;
    [Range(0, 100)] public int probabilidadRed;
    [Range(0, 100)] public int probabilidadResortera;
    [Range(0, 100)] public int probabilidadVaraMagica;
    [Range(0, 100)] public int probabilidadEspadaMadera;
    public int danioRed, danioResortera, danioEspadaMadera, curacionVaraMagica;
    public int usosMaximosVaraMagica;
    private int usosRestantesVaraMagica;
    public int penalizacionPorFallo, penalizacionMaxima;
    private int penalizacionActual = 0;
    private int rachaCorrectas = 0;
    private bool batallaTerminada = false;

    private void Start()
    {
        if (sfxManager == null) sfxManager = FindAnyObjectByType<SFXManagerAaron>();
        usosRestantesVaraMagica = usosMaximosVaraMagica;
        esTurnoJugador = true;
        ActualizarUI();
        MostrarMensajeAaron("Tu turno", colorMensajeNeutral);
    }

    public void MostrarMensajeAaron(string mensaje, Color color)
    {
        if (feedbackMensaje != null) feedbackMensaje.MostrarMensajeAaron(mensaje, color);
        else if (textoMensaje != null) textoMensaje.text = mensaje;
    }

    private void MostrarImpactoAaron(RectTransform punto)
    {
        if (impacto == null || punto == null) return;
        impacto.GetComponent<RectTransform>().anchoredPosition = punto.anchoredPosition;
        impacto.SetActive(false);
        impacto.SetActive(true);
    }

    public void MostrarMensajeSinUsosVaraAaron()
    {
        MostrarMensajeAaron("¡Ya no tienes mas usos!", colorMensajeMalo);
    }

    public bool EstaBatallaTerminadaAaron()
    {
        return batallaTerminada;
    }

    public bool EsTurnoJugadorAaron()
    {
        return esTurnoJugador;
    }

    public bool TieneUsosVaraMagicaDisponiblesAaron()
    {
        return usosRestantesVaraMagica > 0;
    }

    public int ObtenerUsosRestantesVaraMagicaAaron()
    {
        return usosRestantesVaraMagica;
    }

    public int ObtenerRachaCorrectasAaron()
    {
        return rachaCorrectas;
    }

    public int ObtenerPenalizacionActualAaron()
    {
        return penalizacionActual;
    }

    public float ObtenerMultiplicadorActualAaron()
    {
        if (rachaCorrectas <= 0) return 1f;
        if (rachaCorrectas == 1) return 1.3f;
        return 1.5f;
    }

    public void ReiniciarRachaCorrectasAaron()
    {
        rachaCorrectas = 0;
        ActualizarUI();
    }

    public void RegistrarRespuestaCorrectaAaron()
    {
        rachaCorrectas++;
        penalizacionActual = 0;
        ActualizarUI();
    }

    public void RegistrarRespuestaIncorrectaAaron()
    {
        rachaCorrectas = 0;
        penalizacionActual = Mathf.Min(penalizacionActual + penalizacionPorFallo, penalizacionMaxima);
        ActualizarUI();
    }

    public void AtacarConRed()
    {
        if (batallaTerminada || !esTurnoJugador) return;
        IntentarDanioAlEnemigo(danioRed, probabilidadRed);
    }

    public void AtacarConResortera()
    {
        if (batallaTerminada || !esTurnoJugador) return;
        IntentarDanioAlEnemigo(danioResortera, probabilidadResortera);
    }

    public void AtacarConEspadaMadera()
    {
        if (batallaTerminada || !esTurnoJugador) return;
        IntentarDanioAlEnemigo(danioEspadaMadera, probabilidadEspadaMadera);
    }

    public void UsarVaraMagica()
    {
        if (batallaTerminada || !esTurnoJugador) return;
        if (usosRestantesVaraMagica <= 0)
        {
            MostrarMensajeAaron("¡Ya no tienes mas usos!", colorMensajeMalo);
            ActualizarUI();
            return;
        }
        usosRestantesVaraMagica--;
        int probFinal = Mathf.Clamp(probabilidadVaraMagica - penalizacionActual, 0, 100);
        if (Random.Range(1, 101) <= probFinal)
        {
            jugador.Curar(curacionVaraMagica);
            MostrarMensajeAaron("¡Te curaste!", colorMensajeBueno);
            if (sfxManager != null) sfxManager.SonarCuracionAaron();
            if (feedbackVidaJugador != null) feedbackVidaJugador.MostrarTextoAaron("+" + curacionVaraMagica, feedbackVidaJugador.colorCuracion);
        }
        else
        {
            MostrarMensajeAaron("¡La Vara mágica falló!", colorMensajeMalo);
        }
        ActualizarUI();
    }

    private void IntentarDanioAlEnemigo(int danioBase, int probabilidadBase)
    {
        int probFinal = Mathf.Clamp(probabilidadBase - penalizacionActual, 0, 100);
        if (Random.Range(1, 101) <= probFinal)
        {
            int danioFinal = Mathf.RoundToInt(danioBase * ObtenerMultiplicadorActualAaron());
            StartCoroutine(SecuenciaDanioAlEnemigoAaron(danioFinal));
        }
        else
        {
            MostrarMensajeAaron("¡Tu ataque falló!", colorMensajeMalo);
            ActualizarUI();
        }
    }

    private IEnumerator SecuenciaDanioAlEnemigoAaron(int danio)
    {
        if (animatorJugador != null) animatorJugador.SetTrigger("Atacar");
        MostrarMensajeAaron("¡Acertaste!", colorMensajeBueno);
        float mitad = tiempoAnimacionAtaqueJugador * 0.5f;
        yield return new WaitForSeconds(mitad);
        if (sfxManager != null) sfxManager.SonarGolpeAaron();
        MostrarImpactoAaron(puntoImpactoEnemigo);
        yield return new WaitForSeconds(tiempoAnimacionAtaqueJugador - mitad);
        if (animatorEnemigo != null) animatorEnemigo.SetTrigger("RecibirDanio");
        enemigo.RecibirDanio(danio);
        if (feedbackVidaEnemigo != null) feedbackVidaEnemigo.MostrarTextoAaron("-" + danio, feedbackVidaEnemigo.colorDanio);
        ActualizarUI();
        if (enemigo.EstaDerrotado())
        {
            batallaTerminada = true;
            esTurnoJugador = false;
            StartCoroutine(SecuenciaVictoriaAaron());
        }
    }

    public void RecibirAtaqueEnemigoAaron(int danio, string nombreAtaque)
    {
        StartCoroutine(SecuenciaRecibirAtaqueEnemigoAaron(danio, nombreAtaque));
    }

    private IEnumerator SecuenciaRecibirAtaqueEnemigoAaron(int danio, string nombreAtaque)
    {
        if (animatorEnemigo != null) animatorEnemigo.SetTrigger("Atacar");
        MostrarMensajeAaron("El enemigo usó " + nombreAtaque, colorMensajeMalo);
        float mitad = tiempoAnimacionAtaqueEnemigo * 0.5f;
        yield return new WaitForSeconds(mitad);
        if (sfxManager != null) sfxManager.SonarGolpeAaron();
        MostrarImpactoAaron(puntoImpactoJugador);
        yield return new WaitForSeconds(tiempoAnimacionAtaqueEnemigo - mitad);
        if (animatorJugador != null) animatorJugador.SetTrigger("RecibirDanio");
        jugador.RecibirDanio(danio);
        if (feedbackVidaJugador != null) feedbackVidaJugador.MostrarTextoAaron("-" + danio, feedbackVidaJugador.colorDanio);
        ActualizarUI();
        if (jugador.EstaDerrotado())
        {
            batallaTerminada = true;
            esTurnoJugador = false;
            StartCoroutine(SecuenciaDerrotaAaron());
        }
    }

    public void FallarAtaqueEnemigoAaron(string nombreAtaque)
    {
        MostrarMensajeAaron("¡El enemigo falló!", colorMensajeBueno);
        ActualizarUI();
    }

    public void IniciarTurnoEnemigoAaron()
    {
        if (batallaTerminada || enemigo == null || enemigo.EstaDerrotado()) return;
        esTurnoJugador = false;
        StartCoroutine(EjecutarTurnoEnemigoAaron());
    }

    private IEnumerator EjecutarTurnoEnemigoAaron()
    {
        MostrarMensajeAaron("Turno del enemigo", colorMensajeNeutral);
        ActualizarUI();
        yield return new WaitForSeconds(tiempoEsperaTurnoEnemigo);
        if (batallaTerminada || enemigo == null || enemigo.EstaDerrotado()) yield break;
        if (administradorEnemigo != null) administradorEnemigo.EjecutarAtaqueAleatorioAaron();
        else RecibirAtaqueEnemigoAaron(10, "Ataque salvaje");
        if (!batallaTerminada)
        {
            yield return new WaitForSeconds(tiempoDespuesAccionEnemigo);
            esTurnoJugador = true;
            MostrarMensajeAaron("Tu turno", colorMensajeNeutral);
            ActualizarUI();
        }
    }

    public void AtacarConMegaAtaqueAaron(int danio, string nombreAtaque)
    {
        if (batallaTerminada || !esTurnoJugador) return;
        StartCoroutine(SecuenciaMegaAtaqueAaron(danio));
    }

    private IEnumerator SecuenciaMegaAtaqueAaron(int danio)
    {
        if (animatorJugador != null) animatorJugador.SetTrigger("Atacar");
        MostrarMensajeAaron("¡Super arma!", colorMensajeBueno);
        float mitad = tiempoAnimacionAtaqueJugador * 0.5f;
        yield return new WaitForSeconds(mitad);
        if (sfxManager != null) sfxManager.SonarMegaAtaqueAaron();
        MostrarImpactoAaron(puntoImpactoEnemigo);
        yield return new WaitForSeconds(tiempoAnimacionAtaqueJugador - mitad);
        if (animatorEnemigo != null) animatorEnemigo.SetTrigger("RecibirDanio");
        enemigo.RecibirDanio(danio);
        if (feedbackVidaEnemigo != null) feedbackVidaEnemigo.MostrarTextoAaron("-" + danio, feedbackVidaEnemigo.colorDanio);
        ActualizarUI();
        if (enemigo.EstaDerrotado())
        {
            batallaTerminada = true;
            esTurnoJugador = false;
            StartCoroutine(SecuenciaVictoriaAaron());
        }
        else
        {
            IniciarTurnoEnemigoAaron();
        }
    }

    private IEnumerator SecuenciaVictoriaAaron()
    {
        MostrarMensajeAaron("¡Ganaste!", colorMensajeBueno);
        ActualizarUI();
        yield return new WaitForSeconds(tiempoAntesAnimacionFinal);
        if (animatorEnemigo != null) animatorEnemigo.SetTrigger("Morir");
        yield return new WaitForSeconds(tiempoMuerteEnemigo);
        AdministradorResultadoAaron.resultadoFinal = "¡Ganaste!";
        SceneManager.LoadScene("FinDelJuego");
    }

    private IEnumerator SecuenciaDerrotaAaron()
    {
        MostrarMensajeAaron("¡Perdiste!", colorMensajeMalo);
        ActualizarUI();
        yield return new WaitForSeconds(tiempoAntesAnimacionFinal);
        if (visualJugador != null)
        {
            Vector3 escala = visualJugador.localScale;
            escala.x = -Mathf.Abs(escala.x);
            visualJugador.localScale = escala;
        }
        if (animatorJugador != null) animatorJugador.SetTrigger("Huir");
        if (visualJugador != null)
        {
            Vector3 posFinal = visualJugador.position + Vector3.left * distanciaHuidaJugador;
            while (Vector3.Distance(visualJugador.position, posFinal) > 0.05f)
            {
                visualJugador.position = Vector3.MoveTowards(visualJugador.position, posFinal, velocidadHuidaJugador * Time.deltaTime);
                yield return null;
            }
        }
        yield return new WaitForSeconds(tiempoDespuesHuidaJugador);
        AdministradorResultadoAaron.resultadoFinal = "¡Perdiste!";
        SceneManager.LoadScene("FinDelJuego");
    }

    public void ActualizarUI()
    {
        if (textoPorcentajeJugador != null) textoPorcentajeJugador.text = jugador.ObtenerPorcentajeVida().ToString("0") + "%";
        if (textoPorcentajeEnemigo != null) textoPorcentajeEnemigo.text = enemigo.ObtenerPorcentajeVida().ToString("0") + "%";
        if (textoATQ != null) textoATQ.text = "ATQ x" + ObtenerMultiplicadorActualAaron().ToString("0.0");
        if (textoPenalizacion != null)
        {
            if (penalizacionActual > 0)
            {
                textoPenalizacion.text = "-" + penalizacionActual + "%";
                textoPenalizacion.gameObject.SetActive(true);
            }
            else
            {
                textoPenalizacion.text = "";
                textoPenalizacion.gameObject.SetActive(false);
            }
        }
        if (controlVidaJugador != null) controlVidaJugador.ActualizarImagenVida();
        if (controlVidaEnemigo != null) controlVidaEnemigo.ActualizarImagenVida();
    }
}