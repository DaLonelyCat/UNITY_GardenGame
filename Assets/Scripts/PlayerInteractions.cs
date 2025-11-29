using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private int cropsCollected = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Player is hurt");
        
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 3f, ForceMode2D.Impulse);
        }
        else if (other.CompareTag("Crop"))
        {
            Destroy(other.gameObject);
            cropsCollected++;
            Debug.Log("Crop harvested: " + cropsCollected);
        }
        else if (other.CompareTag("Animal"))
        {
            Debug.Log("MOO");
        }
    }
}