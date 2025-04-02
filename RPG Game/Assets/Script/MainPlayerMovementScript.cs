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

    // Global flag to lock movement
    public static bool dialouge = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        

        // Ensure we have a camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Adjust starting position to prevent falling through ground
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

        // Initially lock the cursor (FPS style)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // If dialogue is active, disable movement & show cursor
        if (dialouge)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (animator != null)
            {
                animator.SetBool("IsSprinting", false);
                animator.SetBool("MovingF", false);
                animator.SetBool("MovingB", false);
                animator.SetBool("MovingL", false);
                animator.SetBool("MovingR", false);
            }

            // Skip movement logic
            return;
        }
        else
        {
            // Normal locked-cursor mode
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // ----- Movement & Rotation Logic (only runs if dialouge == false) -----

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        float horizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > movementThreshold 
            ? Input.GetAxisRaw("Horizontal") 
            : 0f;
        float vertical = Mathf.Abs(Input.GetAxisRaw("Vertical")) > movementThreshold 
            ? Input.GetAxisRaw("Vertical") 
            : 0f;

        Vector3 moveDirection = transform.right * (horizontal * horizontalMovementMultiplier)
                              + transform.forward * vertical;
        moveDirection = moveDirection.normalized;

        bool isMoving = moveDirection.magnitude > 0;

        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
            
            // Set directional movement booleans
            animator.SetBool("IsSprinting", vertical > 0);
            animator.SetBool("MovingB", vertical < 0);
            animator.SetBool("MovingL", horizontal > 0);
            animator.SetBool("MovingR", horizontal < 0);
        }

        if (isMoving)
        {
            controller.Move(moveDirection * sprintSpeed * Time.deltaTime);
        }

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
}