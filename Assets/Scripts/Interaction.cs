using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private int cropsCollected = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            animator.SetTrigger("Hurt");
            Debug.Log("Player is hurt");
            
            Vector2 recoilDirection = (transform.position - other.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(recoilDirection * 5f, ForceMode2D.Impulse);
        }
        else if (other.CompareTag("Crop"))
        {
            Destroy(other.gameObject);
            cropsCollected++;
            Debug.Log("Crop harvested: " + cropsCollected);
        }
        else if (other.CompareTag("Animal"))
        {
            if (other.name.Contains("Cow"))
                Debug.Log("MOO");
            else if (other.name.Contains("Sheep")) 
                Debug.Log("BAA");
            else if (other.name.Contains("Chicken"))
                Debug.Log("CLUCK");
        }
    }
}