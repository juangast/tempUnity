using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AdministradorFinalAaron : MonoBehaviour
{
    public TextMeshProUGUI textoResultado;
    public Color colorVictoria = Color.green;
    public Color colorDerrota = Color.red;
    public Color colorNormal = Color.white;
    public AudioClip clipVictoria;
    public AudioClip clipDerrota;
    [Range(0f, 1f)] public float volumenResultado = 0.5f;

    private void Start()
    {
        if (textoResultado == null) return;
        textoResultado.text = AdministradorResultadoAaron.resultadoFinal;

        if (AdministradorResultadoAaron.resultadoFinal == "¡Ganaste!")
        {
            textoResultado.color = colorVictoria;
            ReproducirSonido(clipVictoria);
        }
        else if (AdministradorResultadoAaron.resultadoFinal == "¡Perdiste!")
        {
            textoResultado.color = colorDerrota;
            ReproducirSonido(clipDerrota);
        }
        else
        {
            textoResultado.color = colorNormal;
        }
    }

    private void ReproducirSonido(AudioClip clip)
    {
        if (clip == null) return;
        AudioSource source = GetComponent<AudioSource>();
        if (source == null) source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.PlayOneShot(clip, volumenResultado);
    }

    public void VolverAlMenuAaron()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuAaron");
    }

    public void ReiniciarCombateAaron()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Combate");
    }

    public void SalirJuegoAaron()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}