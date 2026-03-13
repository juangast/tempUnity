using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private UIController_Juan uiController;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackBoxSize = new Vector2(1f, 1f);
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int attackDamage = 1;

    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    private bool isAttacking = false;
    private bool isTakingDamage = false;
    private bool canTakeDamage = true;
    private bool isDead = false;

    private Vector2 lastDirection = Vector2.down;
    [SerializeField] private int hitsPerLife = 5;
    private int currentHits = 0;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        if (!isAttacking && !isTakingDamage)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveInput = new Vector2(moveX, moveY).normalized;

            if (moveInput != Vector2.zero)
            {
                lastDirection = moveInput;
            }

            playerAnimator.SetFloat("Horizontal", moveX);
            playerAnimator.SetFloat("Vertical", moveY);
            playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
        else
        {
            moveInput = Vector2.zero;
            playerAnimator.SetFloat("Speed", 0);
        }

        if (attackPoint != null)
        {
            attackPoint.localPosition = lastDirection;
        }

        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking && !isTakingDamage)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }

    void Attack()
    {
        isAttacking = true;

        Collider2D[] enemigosGolpeados = Physics2D.OverlapBoxAll(
            attackPoint.position,
            attackBoxSize,
            0f,
            enemyLayer
        );

        foreach (Collider2D enemigoCollider in enemigosGolpeados)
        {
            VidaEnemigo_Juan enemigo = enemigoCollider.GetComponent<VidaEnemigo_Juan>();

            if (enemigo == null)
            {
                enemigo = enemigoCollider.GetComponentInParent<VidaEnemigo_Juan>();
            }

            if (enemigo != null)
            {
                enemigo.TakeDamage(attackDamage);
                if (SFXController_Juan.Instance != null)
                    SFXController_Juan.Instance.PlayHit();
            }
        }

        Invoke(nameof(EndAttack), 0.25f);
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int hits = 1)
    {
        if (!canTakeDamage || isDead) return;

        canTakeDamage = false;
        isTakingDamage = true;

        currentHits += hits;

        if (currentHits >= hitsPerLife)
        {
            currentHits = 0;

            if (uiController != null)
            {
                uiController.LoseLife();

                if (uiController.GetLives() <= 0)
                {
                    isDead = true;
                }
            }
        }

        Invoke(nameof(EndDamage), 0.2f);
        Invoke(nameof(ResetDamageCooldown), damageCooldown);
    }

    void EndDamage()
    {
        isTakingDamage = false;
    }

    void ResetDamageCooldown()
    {
        canTakeDamage = true;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackPoint.position, attackBoxSize);
    }
}