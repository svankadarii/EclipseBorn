using UnityEngine;

public class MainPlayerMovementScript : MonoBehaviour
{
  [Header("Movement Settings")]
public float sprintSpeed = 10f;
public float horizontalMovementMultiplier = 0.5f;
public float mouseSensitivity = 3f;
public float movementThreshold = 0.05f;

[Header("Camera Reference")]
public Camera mainCamera;

private CharacterController controller;
private Animator animator;
private float currentRotationX = 0f;

void Start()
{
    // Get required components
    controller = GetComponent<CharacterController>();
    animator = GetComponentInChildren<Animator>();

    // Ensure camera is set
    if (mainCamera == null)
    {
        mainCamera = Camera.main;
    }

    // Lock and hide cursor
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

void Update()
{
    // Mouse look rotation
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    transform.Rotate(Vector3.up * mouseX);

    // Get input with reduced sensitivity
    float horizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > movementThreshold 
        ? Input.GetAxisRaw("Horizontal") 
        : 0f;
    float vertical = Mathf.Abs(Input.GetAxisRaw("Vertical")) > movementThreshold 
        ? Input.GetAxisRaw("Vertical") 
        : 0f;

    // Calculate movement direction with reduced horizontal movement
    Vector3 moveDirection = transform.right * (horizontal * horizontalMovementMultiplier) + 
                             transform.forward * vertical;
    moveDirection = moveDirection.normalized;

    // Check for movement
    bool isMoving = moveDirection.magnitude > 0;

    // Update animator
    if (animator != null)
    {
        
        
        // Set directional movement booleans
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsSprinting", vertical>0);
        animator.SetBool("MovingB", vertical < 0);
        animator.SetBool("MovingR", horizontal < 0);
        animator.SetBool("MovingL", horizontal > 0);
    }

    // Handle movement if input detected
    if (isMoving)
    {
        // Move the character
        controller.Move(moveDirection * sprintSpeed * Time.deltaTime);
    }

    // Simple camera follow
    UpdateCameraPosition();
}
void UpdateCameraPosition()
{
    if (mainCamera == null) return;

    // Basic camera positioning
    Vector3 targetPosition = transform.position 
                             + transform.forward * -5f 
                             + Vector3.up * 2f;

    mainCamera.transform.position = targetPosition;
    mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
}

// Optional: Allow exiting cursor lock
void OnApplicationFocus(bool hasFocus)
{
    if (!hasFocus)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

// ... rest of the script remains the same
}