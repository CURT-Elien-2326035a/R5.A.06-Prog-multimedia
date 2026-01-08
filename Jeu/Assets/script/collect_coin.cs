using UnityEngine;

public class collect_coin : MonoBehaviour
{
    public int coinValue = 1;
    public GameObject collectEffectPrefab; // Drag the particle prefab here

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add score
            GameManager.Instance.AddScore(coinValue);

            // Spawn particles
            if (collectEffectPrefab != null)
            {
                GameObject effect = Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 2f); // remove effect after 2 seconds
            }

            // Destroy coin
            Destroy(gameObject);
        }
    }
}
