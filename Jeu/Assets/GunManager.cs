using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager Instance;

    public bool HasGun { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GiveGun(GameObject gunPrefab, Transform holder)
    {
        if (HasGun)
        {
            Debug.Log("Player already has a gun!");
            return;
        }

        Instantiate(gunPrefab, holder.position, holder.rotation, holder);
        HasGun = true;
    }
}
