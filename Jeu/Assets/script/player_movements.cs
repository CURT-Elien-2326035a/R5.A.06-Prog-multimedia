using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // --- Get input axes ---
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // --- Movement relative to camera ---
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Ignore camera tilt (keep player on ground plane)
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camForward * vertical + camRight * horizontal).normalized;

        // --- Move the player ---
        controller.SimpleMove(move * moveSpeed);

        if (transform.position.y < -20f)
        {
            Die();
        }
    }

    // Called when the player falls below a certain Y position
    void Die()
    {
        // Example: respawn at origin, or implement your own logic
        transform.position = new Vector3(1.1885591f, 3.930043f, -6.079597f);
        // Optionally reset velocity or add more logic here
    }

}

