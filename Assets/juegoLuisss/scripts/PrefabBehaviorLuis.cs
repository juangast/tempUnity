using UnityEngine;

public class PrefabBehaviorLuis : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerBehaviorLuis player = collision.gameObject.GetComponent<PlayerBehaviorLuis>();
            if (player != null) player.PlayDamageFlash();
            Destroy(gameObject);
            GameControlLuis.Instance.sfxManager.PlayDamage();
            GameControlLuis.Instance.SpendLives();
        }
    }
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && transform.position.x < player.transform.position.x - 50f)
        {
            Destroy(gameObject);
        }
    }

    
}
