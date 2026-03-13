using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIControllerLuis : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI pointsText;
    public Sprite spendLives;
    public Image[] livesImage;

    public GameObject questionPanel;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI questionText;
    public Button continueButton;
    public Button[] answerButtons;
    public TextMeshProUGUI[] answerTexts;
    public TextMeshProUGUI ghostPointsText;
    public GameObject hudPanel;
    public Image hudPanelImage;
    public GameObject continueText;

    int lives = 3;
    int time;
    private Vector3 ghostStartPos;
    private Coroutine ghostCoroutine;
    private int ghostAccumulated;
    private float ghostLastTime;
    void Start()
    {
        lives = PlayerPrefs.GetInt("Lives");
        ActiveText();
        UpdatePoints();
        if (questionPanel != null)
            questionPanel.SetActive(false);
        if (ghostPointsText != null)
        {
            ghostStartPos = ghostPointsText.rectTransform.anchoredPosition;
            ghostPointsText.alpha = 0f;
        }
    }

    public void ActiveText()
    {
        timeText.text = "Tiempo: " + time;
    }

    public void StartTimer()
    {
        StartCoroutine(MatchTime());
    }

    public void UpdateLives()
    {
        lives = GameControlLuis.Instance.GetCurrentLives();
        if(lives >= 0 && lives < livesImage.Length)
        {
            livesImage[lives].sprite = spendLives;
        }
        GameControlLuis.Instance.CheckGameOver();
    }

    IEnumerator MatchTime()
    {
        yield return new WaitForSeconds(1);
        time += 1;
        ActiveText();
        StartCoroutine(MatchTime());
    }

    public void UpdatePoints()
    {
        if(pointsText != null)
        {
            pointsText.text = "Puntos: " + PlayerPrefs.GetInt("Points");
        }
    }

    public void ShowGhostPoints(int amount)
    {
        if (ghostPointsText == null) return;

        if (ghostCoroutine != null)
            StopCoroutine(ghostCoroutine);

        if (Time.time - ghostLastTime < 0.3f)
            ghostAccumulated += amount;
        else
            ghostAccumulated = amount;

        ghostLastTime = Time.time;

        ghostPointsText.text = "+" + ghostAccumulated;
        ghostPointsText.alpha = 1f;
        ghostPointsText.rectTransform.anchoredPosition = ghostStartPos;
        ghostCoroutine = StartCoroutine(FadeGhostText());
    }

    IEnumerator FadeGhostText()
    {
        float duration = 1.5f;
        float elapsed = 0f;
        float moveDistance = 50f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            ghostPointsText.alpha = 1f - t;
            ghostPointsText.rectTransform.anchoredPosition = ghostStartPos + Vector3.up * (moveDistance * t);
            yield return null;
        }

        ghostPointsText.alpha = 0f;
        ghostPointsText.rectTransform.anchoredPosition = ghostStartPos;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuSceneLuis");
    }
}
