using UnityEngine;

public class CharacterMovementUpperWorld : MonoBehaviour
{
    Rigidbody2D playerRigidBody;
    bool isGrounded;
    public float horizontalInput;
    public float speed = 10.0f;

    public int maxJumps = 2;        // Total allowed jumps (e.g., 2 = double jump)
    private int jumpsRemaining;     // Track how many jumps are left

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            playerRigidBody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); // Inverted jump
            jumpsRemaining--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps; // Reset jump count when grounded
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}