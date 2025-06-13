using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int healthPoints = 3;

    [Tooltip("Assign the Game Over Canvas here")]
    public GameObject deathCanvas;

    [Tooltip("Drag here the movement script or any other component to disable on death")]
    public MonoBehaviour[] componentsToDisable;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ReceiveDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }

        // Disable movement/control scripts
        foreach (var comp in componentsToDisable)
        {
            comp.enabled = false;
        }

        // Hide sprite
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        // Optional: disable collider
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Freeze physics
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

}
