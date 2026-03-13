using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManagerLuis : MonoBehaviour
{
    public static SFXManagerLuis Instance;

    public AudioClip coinSFX;
    public AudioClip damageSFX;
    public AudioClip loseSFX;
    public AudioClip questionBoxSFX;
    public AudioClip menuSFX;
    public AudioClip buttonPressSFX;
    public AudioClip correctAnswerSFX;
    public AudioClip wrongAnswerSFX;

    public AudioClip menuMusic;
    public AudioClip difficultySceneMusic;
    public AudioClip gameMusic;

    private AudioSource persistentSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        persistentSource = gameObject.AddComponent<AudioSource>();
        persistentSource.loop = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        AudioClip clip = null;
        if (sceneName == "StartSceneLuis") clip = menuMusic;
        else if (sceneName == "DifficultySceneLuis") clip = difficultySceneMusic;
        else if (sceneName == "FugaSalvajeLuis") clip = gameMusic;
        else if (sceneName == "EndSceneLuis") clip = loseSFX;

        if (clip != null && persistentSource.clip != clip)
        {
            persistentSource.clip = clip;
            persistentSource.Play();
        }
    }

    public void PlayCoin()
    {
        if (coinSFX != null)
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position, 0.5f);
    }

    public void PlayDamage()
    {
        if (damageSFX != null)
            AudioSource.PlayClipAtPoint(damageSFX, Camera.main.transform.position, 0.5f);
    }

    public void PlayLose()
    {
        if (loseSFX != null)
            persistentSource.PlayOneShot(loseSFX, 0.5f);
    }

    public void PlayQuestionBox()
    {
        if (questionBoxSFX != null)
            AudioSource.PlayClipAtPoint(questionBoxSFX, Camera.main.transform.position, 0.5f);
    }

    public void PlayMenu()
    {
        if (menuSFX != null)
            AudioSource.PlayClipAtPoint(menuSFX, Camera.main.transform.position, 0.3f);
    }

    public void PlayButtonPress()
    {
        if (buttonPressSFX != null)
            persistentSource.PlayOneShot(buttonPressSFX, 1f);
    }

    public void PlayCorrectAnswer()
    {
        if (correctAnswerSFX != null)
            persistentSource.PlayOneShot(correctAnswerSFX, 1f);
    }

    public void PlayWrongAnswer()
    {
        if (wrongAnswerSFX != null)
            persistentSource.PlayOneShot(wrongAnswerSFX, 1f);
    }

}
