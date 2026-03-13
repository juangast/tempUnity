using UnityEngine;

public class VidaEnemigo_Juan : MonoBehaviour
{
    public int life = 2;
    public UIController_Juan uiController;

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            if (uiController != null)
            {
                uiController.AddCoin(1);
            }
            Destroy(gameObject);
        }
    }
}