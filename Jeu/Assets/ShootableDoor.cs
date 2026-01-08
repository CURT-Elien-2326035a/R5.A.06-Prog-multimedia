using UnityEngine;

public class ShootableDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public int health = 3;             // number of hits to destroy
    public GameObject destroyEffect;   // optional particle effect

    [Header("Optional")]
    public bool isOpen = false;

    void Start()
    {
        isOpen = false;
    }

    public void TakeDamage(int amount = 1)
    {
        if (isOpen) return;

        health -= amount;

        if (health <= 0)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        // Optional: particle effect
        if (destroyEffect != null)
        {
            GameObject effect = Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(effect, 2f);
        }

        // Remove collider so player can pass
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // Optional: animation or visual removal
        Destroy(gameObject);
        Debug.Log("Door opened!");
    }
}
