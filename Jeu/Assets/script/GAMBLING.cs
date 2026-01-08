using UnityEngine;

public class LuckCube : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public float interactRange = 3f;

    [Header("Gun Reward")]
    public GameObject gunPrefab;
    public Transform gunHolder;

    [Header("UI")]
    public UIPopup popup;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!player) return;

        if (Vector3.Distance(player.position, transform.position) <= interactRange &&
            Input.GetKeyDown(interactKey))
        {
            ApplyLuck();
        }
    }

    void ApplyLuck()
    {
        float roll = Random.value;

        if (roll < 0.10f)
        {
            GunManager.Instance.GiveGun(gunPrefab, gunHolder);
            popup.Show("🔫 You got a gun!");
        }
        else if (roll < 0.50f)
        {
            GameManager.Instance.AddScore(GameManager.Instance.CurrentScore);
            popup.Show("🍀 Score doubled!");
        }
        else if (roll < 0.75f)
        {
            GameManager.Instance.SetScore(Mathf.RoundToInt(GameManager.Instance.CurrentScore * 0.5f));
            popup.Show("💀 Score halved!");
        }
        else
        {
            popup.Show("😐 Nothing happened");
        }

        gameObject.SetActive(false);
    }
}
