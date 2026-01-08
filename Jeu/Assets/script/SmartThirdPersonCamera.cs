using UnityEngine;

public class SmartThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("UI")]
    public GameObject homeScreen; // Assign your home screen UI panel in inspector
    public GameObject winScreen; // Assign your win screen UI panel in inspector

    [Header("Camera Settings")]
    public float distance = 5f;
    public float minDistance = 1.0f;
    public float maxDistance = 6.0f;
    public float height = 2f;
    public float rotationSpeed = 3f;
    public float followSmoothness = 10f;
    public float autoAlignSpeed = 2f;

    [Header("Collision Settings")]
    public LayerMask collisionMask;
    public float collisionOffset = 0.3f;

    private float yaw;
    private float pitch = 20f;
    private Vector3 currentVelocity;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        // Don't capture mouse if homescreen is active
        if (homeScreen != null && homeScreen.activeSelf)
        {
            return;
        }

        // Don't capture mouse if winscreen is active
        if (winScreen != null && winScreen.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        // Capture mouse on left click
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
{
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

        if (!target) return;

        // --- Mouse control ---
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float moveInput = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

        // Rotate with mouse when moving it
        if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
        {
            yaw += mouseX * rotationSpeed;
            pitch -= mouseY * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -20f, 60f);
        }
        else if (moveInput > 0.1f) // Auto-align behind player when moving
        {
            float targetYaw = target.eulerAngles.y;
            yaw = Mathf.LerpAngle(yaw, targetYaw, Time.deltaTime * autoAlignSpeed);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // --- Apply camera yaw to the target's Y rotation (only when grounded) ---
        if (target != null)
        {
            CharacterController targetController = target.GetComponent<CharacterController>();
            Quaternion targetYawRot = Quaternion.Euler(0f, yaw, 0f);

            if (targetController != null)
            {
                if (targetController.isGrounded)
                {
                    target.rotation = Quaternion.Slerp(target.rotation, targetYawRot, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                // If no CharacterController, still apply rotation
                target.rotation = Quaternion.Slerp(target.rotation, targetYawRot, rotationSpeed * Time.deltaTime);
            }
        }

        // Desired camera position
        Vector3 desiredPos = target.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // --- Collision check ---
        RaycastHit hit;
        if (Physics.Linecast(target.position + Vector3.up * height, desiredPos, out hit, collisionMask))
        {
            float adjustedDistance = Vector3.Distance(target.position + Vector3.up * height, hit.point) - collisionOffset;
            adjustedDistance = Mathf.Clamp(adjustedDistance, minDistance, maxDistance);
            desiredPos = target.position - (rotation * Vector3.forward * adjustedDistance) + Vector3.up * height;
        }

        // --- Smooth follow ---
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref currentVelocity, 1f / followSmoothness);
        transform.rotation = rotation;
    }
}
