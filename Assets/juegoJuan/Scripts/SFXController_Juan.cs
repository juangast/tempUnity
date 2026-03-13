using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXController_Juan : MonoBehaviour
{
    public static SFXController_Juan Instance;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    [Header("Background Music")]
    public AudioClip menuSound;
    public AudioClip gameSound;
    public AudioClip loseSound;
    public AudioClip victorySound;

    [Header("Sound Effects")]
    public AudioClip hitSound;
    public AudioClip itemSound;
    public AudioClip coinSound;
    public AudioClip magicBookSound;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (Instance == this)
            Instance = null;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenuTresJuegosJuan")
        {
            Destroy(gameObject);
            return;
        }

        switch (scene.name)
        {
            case "EntrarJuan":
            case "MenuJuan":
                PlayMusic(menuSound);
                break;
            case "JuegoJuan":
            case "TutorialJuan":
                PlayMusic(gameSound);
                break;
            case "EndSceneJuan":
                PlayMusic(loseSound);
                break;
            case "VictoriaSceneJuan":
                PlayMusic(victorySound);
                break;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayHit()
    {
        PlaySFX(hitSound);
    }

    public void PlayItem()
    {
        PlaySFX(itemSound);
    }

    public void PlayCoin()
    {
        PlaySFX(coinSound);
    }

    public void PlayMagicBook()
    {
        PlaySFX(magicBookSound);
    }

    void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }
}
