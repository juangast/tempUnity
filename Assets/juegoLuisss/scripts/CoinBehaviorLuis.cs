using UnityEngine;

public class CoinBehaviorLuis : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameControlLuis.Instance.AddPoints(10);
            GameControlLuis.Instance.uiController.ShowGhostPoints(10);
            GameControlLuis.Instance.sfxManager.PlayCoin();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (transform.position.x < player.transform.position.x - 50f)
        {
            Destroy(gameObject);
        }
    }
}
