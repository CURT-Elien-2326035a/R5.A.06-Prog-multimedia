using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public int maxAmmo = 10;
    public float fireRate = 0.3f;
    public float range = 100f;
    public bool infiniteAmmo = true;

    [Header("References")]
    public Transform muzzle;
    public TextMeshProUGUI ammoText;

    private int currentAmmo;
    private float nextFireTime;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (!infiniteAmmo && currentAmmo <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        nextFireTime = Time.time + fireRate;
        
        if (!infiniteAmmo)
        {
            currentAmmo--;
        }
        
        UpdateAmmoUI();

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            
            ShootableDoor door = hit.collider.GetComponent<ShootableDoor>();
            if (door != null)
            {
                door.TakeDamage(1); 
            }
        }

        Debug.Log("Bang!");
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            if (infiniteAmmo)
                ammoText.text = "Ammo: ∞";
            else
                ammoText.text = "Ammo: " + currentAmmo;
        }
    }
}