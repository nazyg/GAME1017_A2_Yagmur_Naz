using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Hareket
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        // Koşma animasyonu
        animator.SetBool("isrunning", Mathf.Abs(horizontal) > 0);

        // Sprite yönü
        if (horizontal != 0)
            spriteRenderer.flipX = horizontal < 0;

        // Zıplama (Boşluktayken tekrar zıplamasın diye isGrounded kontrolü)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("jump", true);
        }

        // Slide animasyonu (S tuşuna basılıyken)
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("slide", true);
        }
        else
        {
            animator.SetBool("slide", false);
        }

        // Ölme animasyonu (L tuşuna basınca)
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("die");
        }

        // Yerde olup olmadığını kontrol et
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            isGrounded = true;
            animator.SetBool("jump", false);
        }
        else
        {
            isGrounded = false;
        }
    }
}
