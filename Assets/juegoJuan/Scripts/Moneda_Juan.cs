using UnityEngine;

public class Moneda_Juan : MonoBehaviour
{
    public UIController_Juan uiController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (SFXController_Juan.Instance != null)
                SFXController_Juan.Instance.PlayCoin();
            uiController.AddCoin(1);
            Destroy(gameObject);
        }
    }
}