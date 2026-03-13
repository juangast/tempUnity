using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneControllerLuis : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    void Start()
    {
        int basePoints = PlayerPrefs.GetInt("Points");
        float multiplier = PlayerPrefs.GetFloat("ScoreMultiplier", 1f);
        string difficultyName = PlayerPrefs.GetString("DifficultyName", "Facil");
        int total = Mathf.RoundToInt(basePoints * multiplier);

        pointsText.text = "Puntos: " + basePoints + " x " + multiplier + " (" + difficultyName + ") = " + total;
    }

    public void RestartGame()
    {
        SFXManagerLuis.Instance.PlayButtonPress();
        SceneManager.LoadScene("DifficultySceneLuis");
    }

    public void LeaveGame()
    {
        SFXManagerLuis.Instance.PlayButtonPress();
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
