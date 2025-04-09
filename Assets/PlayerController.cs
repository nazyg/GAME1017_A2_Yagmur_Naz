using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

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

        // Slide animasyonu
        animator.SetBool("slide", Input.GetKey(KeyCode.S));

        // Yerde olup olmadığını kontrol et
        isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.01f;
        animator.SetBool("jump", !isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Taşa çarptı!");
            animator.SetTrigger("die");

            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;

            Invoke("GoToGameOver", 1f);
        }
    }

    void GoToGameOver()
    {
        SceneManager.LoadScene("GameoverScene");
    }
}
