using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySceneManagerLuis : MonoBehaviour
{
    public void SetEasy()
    {
        PlayerPrefs.SetFloat("MoveSpeed", 5f);
        PlayerPrefs.SetFloat("SpeedIncreaseAmount", 4f);
        PlayerPrefs.SetFloat("SpeedIncreaseInterval", 30f);
        PlayerPrefs.SetString("ExcludedObstacles", "treeDown,log,ruinsStone");
        PlayerPrefs.SetFloat("MinSpawnInterval", 2f);
        PlayerPrefs.SetFloat("MaxSpawnInterval", 3f);
        PlayerPrefs.SetFloat("ScoreMultiplier", 1f);
        PlayerPrefs.SetString("DifficultyName", "Facil");
        PlayAndLoad();
    }

    public void SetMedium()
    {
        PlayerPrefs.SetFloat("MoveSpeed", 10f);
        PlayerPrefs.SetFloat("SpeedIncreaseAmount", 8f);
        PlayerPrefs.SetFloat("SpeedIncreaseInterval", 30f);
        PlayerPrefs.SetString("ExcludedObstacles", "ruinsStone");
        PlayerPrefs.SetFloat("MinSpawnInterval", 1.5f);
        PlayerPrefs.SetFloat("MaxSpawnInterval", 2.5f);
        PlayerPrefs.SetFloat("ScoreMultiplier", 1.5f);
        PlayerPrefs.SetString("DifficultyName", "Medio");
        PlayAndLoad();
    }

    public void SetHard()
    {
        PlayerPrefs.SetFloat("MoveSpeed", 15f);
        PlayerPrefs.SetFloat("SpeedIncreaseAmount", 10f);
        PlayerPrefs.SetFloat("SpeedIncreaseInterval", 20f);
        PlayerPrefs.SetString("ExcludedObstacles", "");
        PlayerPrefs.SetFloat("MinSpawnInterval", 0.8f);
        PlayerPrefs.SetFloat("MaxSpawnInterval", 1.8f);
        PlayerPrefs.SetFloat("ScoreMultiplier", 2f);
        PlayerPrefs.SetString("DifficultyName", "Dificil");
        PlayAndLoad();
    }

    private void PlayAndLoad()
    {
        SFXManagerLuis.Instance.PlayButtonPress();
        SceneManager.LoadScene("FugaSalvajeLuis");
    }
}
