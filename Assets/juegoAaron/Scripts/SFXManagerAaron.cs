using UnityEngine;

public class SFXManagerAaron : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip musicaFondo;
    public bool reproducirMusicaAlIniciar = true;
    public AudioClip clipClick, clipCorrecta, clipIncorrecta, clipGolpe;
    public AudioClip clipMegaAtaque, clipCuracion, clipVictoria, clipDerrota, clipAparicionMega;
    [Range(0f, 1f)] public float volumenClick = 1f;
    [Range(0f, 1f)] public float volumenCorrecta = 1f;
    [Range(0f, 1f)] public float volumenIncorrecta = 1f;
    [Range(0f, 1f)] public float volumenGolpe = 1f;
    [Range(0f, 1f)] public float volumenMegaAtaque = 1f;
    [Range(0f, 1f)] public float volumenCuracion = 1f;
    [Range(0f, 1f)] public float volumenVictoria = 1f;
    [Range(0f, 1f)] public float volumenDerrota = 1f;
    [Range(0f, 1f)] public float volumenAparicionMega = 1f;

    private void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (musicSource == null && sources.Length > 0) musicSource = sources[0];
        if (sfxSource == null || sfxSource == musicSource)
        {
            if (sources.Length > 1) sfxSource = sources[1];
            else
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
                sfxSource.playOnAwake = false;
            }
        }
    }

    private void Start()
    {
        if (reproducirMusicaAlIniciar && musicSource != null && musicaFondo != null)
        {
            musicSource.clip = musicaFondo;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    private void Reproducir(AudioClip clip, float volumen)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip, volumen);
    }

    public void SonarClickAaron() { Reproducir(clipClick, volumenClick); }
    public void SonarCorrectaAaron() { Reproducir(clipCorrecta, volumenCorrecta); }
    public void SonarIncorrectaAaron() { Reproducir(clipIncorrecta, volumenIncorrecta); }
    public void SonarGolpeAaron() { Reproducir(clipGolpe, volumenGolpe); }
    public void SonarMegaAtaqueAaron() { Reproducir(clipMegaAtaque, volumenMegaAtaque); }
    public void SonarCuracionAaron() { Reproducir(clipCuracion, volumenCuracion); }
    public void SonarVictoriaAaron() { Reproducir(clipVictoria, volumenVictoria); }
    public void SonarDerrotaAaron() { Reproducir(clipDerrota, volumenDerrota); }
    public void SonarAparicionMegaAaron() { Reproducir(clipAparicionMega, volumenAparicionMega); }
}