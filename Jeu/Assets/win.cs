using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    public GameObject winEffectPrefab; // Optional: special particle effect for winning
    public int bonusScore = 10; // Optional: bonus points for winning coin

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add bonus score
            if (bonusScore > 0)
            {
                GameManager.Instance.AddScore(bonusScore);
            }

            // Spawn win effect
            if (winEffectPrefab != null)
            {
                GameObject effect = Instantiate(winEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 3f);
            }

            // Trigger win condition
            GameManager.Instance.WinGame();

            // Destroy the win coin
            Destroy(gameObject);
        }
    }
}
