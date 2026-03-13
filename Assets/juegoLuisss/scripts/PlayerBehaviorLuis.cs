using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviorLuis : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float jumpCutMultiplier = 0.5f;
    public float fastFallForce = 10f;
    public float speedIncreaseAmount = 0.5f;
    public float speedIncreaseInterval = 30f;
    public Rigidbody2D rig;
    private float speedTimer;
    private bool jumpRequested;
    private bool slideRequested;
    private bool jumpHeld;
    private bool fastFallRequested;
    private SpriteRenderer spriteRenderer;
    private Coroutine damageFlashCoroutine;
    Animator animatorController;
    private bool isGrounded = true;
    private float lastScoredX;
    private BoxCollider2D boxCollider;
    private Vector2 standingSize;
    private Vector2 standingOffset;
    public enum PlayerAnimation
    {
        run, jump, slide
    }
    void Start()
    {
        animatorController = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        standingSize = boxCollider.size;
        standingOffset = boxCollider.offset;
        lastScoredX = transform.position.x;
        moveSpeed = PlayerPrefs.GetFloat("MoveSpeed", moveSpeed);
        speedIncreaseAmount = PlayerPrefs.GetFloat("SpeedIncreaseAmount", speedIncreaseAmount);
        speedIncreaseInterval = PlayerPrefs.GetFloat("SpeedIncreaseInterval", speedIncreaseInterval);
    }
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        speedTimer += Time.deltaTime;
        if (speedTimer >= speedIncreaseInterval)
        {
            moveSpeed += speedIncreaseAmount;
            speedTimer = 0f;
        }

        int metersPassed = Mathf.FloorToInt(transform.position.x - lastScoredX);
        if (metersPassed > 0)
        {
            GameControlLuis.Instance.AddPoints(metersPassed);
            lastScoredX += metersPassed;
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }

        jumpHeld = Keyboard.current.upArrowKey.isPressed;

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            if (isGrounded)
                slideRequested = true;
            else
                fastFallRequested = true;
        }

        UpdatePlayerAnimation();
    }

    void UpdateAnimation(PlayerAnimation nameAnimation)
    {
        switch (nameAnimation)
        {
            case PlayerAnimation.run:
                animatorController.SetBool("isJumping", false);
                animatorController.SetBool("isSliding", false);
                boxCollider.size = standingSize;
                boxCollider.offset = standingOffset;
                break;
            case PlayerAnimation.jump:
                animatorController.SetBool("isJumping", true);
                animatorController.SetBool("isSliding", false);
                boxCollider.size = standingSize;
                boxCollider.offset = standingOffset;
                break;
            case PlayerAnimation.slide:
                animatorController.SetBool("isSliding", true);
                animatorController.SetBool("isJumping", false);
                boxCollider.size = new Vector2(standingSize.x, standingSize.y * 0.5f);
                boxCollider.offset = new Vector2(standingOffset.x, standingOffset.y - standingSize.y * 0.25f);
                break;
        }
    }



    void UpdatePlayerAnimation()
    {
        AnimatorStateInfo stateInfo = animatorController.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("slide") && stateInfo.normalizedTime >= 1f)
            slideRequested = false;

        if (!isGrounded)
            UpdateAnimation(PlayerAnimation.jump);
        else if (slideRequested)
            UpdateAnimation(PlayerAnimation.slide);
        else
            UpdateAnimation(PlayerAnimation.run);
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(rig.linearVelocity.y) < 0.01f)
        {
            isGrounded = true;
        }
        if (jumpRequested && isGrounded)
        {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        if (!jumpHeld && !isGrounded && rig.linearVelocity.y > 0)
        {
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, rig.linearVelocity.y * jumpCutMultiplier);
        }
        if (fastFallRequested && !isGrounded)
        {
            rig.AddForce(Vector2.down * fastFallForce, ForceMode2D.Impulse);
            fastFallRequested = false;
        }
        jumpRequested = false;
    }

    public void PlayDamageFlash()
    {
        if (damageFlashCoroutine != null)
            StopCoroutine(damageFlashCoroutine);
        damageFlashCoroutine = StartCoroutine(DamageFlashRoutine());
    }

    private IEnumerator DamageFlashRoutine()
    {
        spriteRenderer.color = Color.red;
        float duration = 0.4f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, elapsed / duration);
            yield return null;
        }
        spriteRenderer.color = Color.white;
    }
}
