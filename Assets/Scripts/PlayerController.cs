using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    
    [Header("Combat")]
    public float recoilForce = 5f;
    
    private bool isRecoiling = false;
    private float recoilTimer = 0f;
    public float recoilDuration = 0.25f;
    
    [Header("Game State")]
    public int cropsCollected = 0;

    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        if (animator == null)
        {
            Debug.LogWarning("PlayerController: Animator component not found!");
        }
    }

    void Update()
    {
        if (isRecoiling)
        {
            recoilTimer -= Time.deltaTime;
            if (recoilTimer <= 0)
            {
                isRecoiling = false;
            }
            movement = Vector2.zero; 
            return;
        }

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        bool isMoving = (movement.x != 0f || movement.y != 0f);

        if (animator != null)
        {
            animator.SetBool("IsWalking", isMoving);
        }
    }

    void FixedUpdate()
    {
        if (isRecoiling)
        {
            return;
        }
        
        rb.velocity = movement * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("OnCollisionEnter2D hit: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Monster") && !isRecoiling)
        {
            Debug.Log("Player is hurt");

            if (animator != null)
            {
                animator.SetTrigger("Hurt");
            }
            
            Vector2 recoilDirection = (transform.position - collision.transform.position).normalized;
            
            rb.velocity = Vector2.zero; 
            rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Crop crop = other.GetComponent<Crop>();
        if (crop != null)
        {
            cropsCollected++;
            crop.Collect();
            Debug.Log($"Crop harvested: {cropsCollected}");
            return;
        }
        
        if (other.CompareTag("Cow"))
        {
            Debug.Log("MOO");
        }
    }
}