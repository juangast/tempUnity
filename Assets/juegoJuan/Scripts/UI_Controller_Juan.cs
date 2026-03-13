using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController_Juan : MonoBehaviour
{
    public TextMeshProUGUI keysText;
    public TextMeshProUGUI coinsText;
    public Sprite fullLives;
    public Sprite spendLives;
    public Image[] livesImage;

    private int lives = 3;
    private int keys = 0;
    private int coins = 0;

    void Start()
    {
        UpdateKeysUI();
        UpdateCoinsUI();
    }

    public void LoseLife()
    {
        if (lives <= 0) return;

        lives--;

        if(livesImage.Length > lives)
        {
            livesImage[lives].sprite = spendLives;
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("EndSceneJuan");
        }
    }

    public int GetLives()
    {
        return lives;
    }

    public void AddKey(int amount = 1)
    {
        keys += amount;
        UpdateKeysUI();
    }

    void UpdateKeysUI()
    {
        if (keysText != null)
        {
            keysText.text = keys.ToString();
        }
    }

    public bool UseKey()
    {
        if (keys > 0)
        {
            keys--;
            UpdateKeysUI();
            return true;
        }
        return false;
    }

    public int GetKeys()
    {
        return keys;
    }

    public void AddCoin(int amount = 1)
    {
        coins += amount;
        UpdateCoinsUI();
    }

    void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = coins.ToString();
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuJuan");
    }
}