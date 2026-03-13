using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuSceneControllerLuis : MonoBehaviour
{
    public void StartGame()
    {
        SFXManagerLuis.Instance.PlayButtonPress();
        SceneManager.LoadScene("DifficultySceneLuis");
    }

    public void End()
    {
        SFXManagerLuis.Instance.PlayButtonPress();
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
