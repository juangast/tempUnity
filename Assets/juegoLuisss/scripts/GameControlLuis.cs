using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlLuis : MonoBehaviour
{

    
    static public GameControlLuis Instance;
    public UIControllerLuis uiController;
    public SFXManagerLuis sfxManager;

    public void Awake()
    {
       StopAllCoroutines();
       PlayerPrefs.SetInt("Lives", 3);
       PlayerPrefs.SetInt("Points", 0);
       Instance = this;
       Instance.SetReferences();
       DontDestroyOnLoad(this);
       SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiController = FindAnyObjectByType<UIControllerLuis>();
        if (uiController != null) uiController.StartTimer();
    }

    void SetReferences(){
        if(uiController == null)
        {
            uiController = FindAnyObjectByType<UIControllerLuis>();
        }
        if(sfxManager == null)
        {
            sfxManager = FindAnyObjectByType<SFXManagerLuis>();
        }
        init();
        }

    void init()
    {
       if(uiController != null){
            uiController.StartTimer();
        }
    }

    public int GetCurrentLives()
    {
        return PlayerPrefs.GetInt("Lives");
    }

    public void SpendLives()
    {
        if(GetCurrentLives() > 0)
        {
            int newLives = GetCurrentLives() - 1;
            PlayerPrefs.SetInt("Lives", newLives);
            uiController.UpdateLives();
        }

    }
    public void CheckGameOver()
    {
        if(GetCurrentLives() == 0)
        {
            foreach (AudioSource a in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
                if (a.gameObject != sfxManager.gameObject) a.Stop();
            sfxManager.PlayLose();
            SceneManager.LoadScene("EndSceneLuis");
        }
    }

    public void AddPoints(int amount)
    {
        int current = PlayerPrefs.GetInt("Points");
        PlayerPrefs.SetInt("Points", current + amount);
        if(uiController != null)
        {
            uiController.UpdatePoints();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartSceneLuis");
    }

    public int GetPoints()
    {
        return PlayerPrefs.GetInt("Points");
    }

}
