using UnityEngine;
using UnityEngine.SceneManagement;

public class ZonaVictoria_Juan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("VictoriaSceneJuan");
        }
    }
}
