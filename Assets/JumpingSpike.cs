using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpingSpike : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float jumpInterval = 2f;
    public float jumpDuration = 0.5f;

    private Vector3 startPos;
    private bool isJumping = false;

    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("StartJump", jumpInterval, jumpInterval);
    }

    void StartJump()
    {
        if (!isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpRoutine());
        }
    }

    System.Collections.IEnumerator JumpRoutine()
    {
        Vector3 topPos = startPos + Vector3.up * jumpHeight;
        float elapsed = 0f;

        // Move up
        while (elapsed < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPos, topPos, elapsed / jumpDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = topPos;

        // Wait at the top
        yield return new WaitForSeconds(0.5f);

        // Move down
        elapsed = 0f;
        while (elapsed < jumpDuration)
        {
            transform.position = Vector3.Lerp(topPos, startPos, elapsed / jumpDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
        isJumping = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched the spike!");

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Animator playerAnimator = collision.gameObject.GetComponent<Animator>();

            if (playerRb != null && playerAnimator != null)
            {
                playerAnimator.SetTrigger("die");
                playerRb.linearVelocity = Vector2.zero;
                playerRb.bodyType = RigidbodyType2D.Static;

                Invoke("GoToGameOver", 1f);
            }
        }
    }

    void GoToGameOver()
    {
        SceneManager.LoadScene("GameoverScene");
    }
}
