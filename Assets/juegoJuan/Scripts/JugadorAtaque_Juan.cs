using UnityEngine;

public class JugadorAtaque_Juan : MonoBehaviour
{
    public int damage = 1;
    public GameObject espadaVisual;

    private void OnEnable()
    {
        if (espadaVisual != null)
            espadaVisual.SetActive(true);
    }

    private void OnDisable()
    {
        if (espadaVisual != null)
            espadaVisual.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Golpee a: " + other.name);

        VidaEnemigo_Juan enemigo = other.GetComponent<VidaEnemigo_Juan>();

        if (enemigo == null)
        {
            enemigo = other.GetComponentInParent<VidaEnemigo_Juan>();
        }

        if (enemigo != null)
        {
            Debug.Log("Le hice dano");
            enemigo.TakeDamage(damage);
        }
    }
}