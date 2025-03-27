// using UnityEngine;
//
// public class MainPlayerMovementScript : MonoBehaviour
// {
//     [Header("Movement Settings")]
//     public float sprintSpeed = 10f;
//     public float horizontalMovementMultiplier = 0.5f;
//     public float mouseSensitivity = 3f;
//     public float movementThreshold = 0.05f;
//
//     [Header("Camera Reference")]
//     public Camera mainCamera;
//
//     private CharacterController controller;
//     private Animator animator;
//     private float currentRotationX = 0f;
//     
//     // Flag to indicate if dialogue is currently open
//     public static bool dialouge = false;
//
//     void Start()
//     {
//         // Get required components
//         controller = GetComponent<CharacterController>();
//         animator = GetComponentInChildren<Animator>();
//
//         // Ensure camera is set
//         if (mainCamera == null)
//         {
//             mainCamera = Camera.main;
//         }
//
//         // Initially lock the cursor
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;
//     }
//
//     void Update()
//     {
//         // If dialogue is open, disable movement & show cursor (if desired)
//         if (dialouge)
//         {
//             // Optionally unlock/show the cursor for UI interaction
//             Cursor.lockState = CursorLockMode.None;
//             Cursor.visible = true;
//
//             // Skip movement and rotation
//             if (animator != null)
//             {
//                 // Optionally set the animator’s movement states to idle
//                 animator.SetBool("IsSprinting", false);
//             }
//             return;
//         }
//         else
//         {
//             // Normal locked-cursor, first-person mode
//             Cursor.lockState = CursorLockMode.Locked;
//             Cursor.visible = false;
//         }
//
//         // ---- Movement & Rotation Logic (only runs if dialouge == false) ----
//
//         // Mouse look rotation
//         float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//         transform.Rotate(Vector3.up * mouseX);
//
//         // Movement input
//         float horizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > movementThreshold 
//             ? Input.GetAxisRaw("Horizontal") 
//             : 0f;
//         float vertical = Mathf.Abs(Input.GetAxisRaw("Vertical")) > movementThreshold 
//             ? Input.GetAxisRaw("Vertical") 
//             : 0f;
//
//         Vector3 moveDirection = transform.right * (horizontal * horizontalMovementMultiplier)
//                               + transform.forward * vertical;
//         moveDirection = moveDirection.normalized;
//
//         bool isMoving = moveDirection.magnitude > 0;
//
//         // Update animator
//         if (animator != null)
//         {
//             animator.SetBool("IsSprinting", isMoving);
//         }
//
//         // Move the character
//         if (isMoving)
//         {
//             controller.Move(moveDirection * sprintSpeed * Time.deltaTime);
//         }
//
//         // Camera follow
//         UpdateCameraPosition();
//     }
//
//     void FixedUpdate()
//     {
//         // If you had physics-based movement, you'd check dialouge here as well.
//         if (dialouge)
//         {
//             return; // Skip any physics-based movement if dialogue is open
//         }
//
//         // Otherwise do any physics-based movement here
//     }
//
//     void UpdateCameraPosition()
//     {
//         if (mainCamera == null) return;
//
//         // Basic camera positioning
//         Vector3 targetPosition = transform.position 
//             + transform.forward * -5f 
//             + Vector3.up * 2f;
//
//         mainCamera.transform.position = targetPosition;
//         mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
//     }
//
//     void OnApplicationFocus(bool hasFocus)
//     {
//         if (!hasFocus)
//         {
//             Cursor.lockState = CursorLockMode.None;
//             Cursor.visible = true;
//         }
//     }
// }
// using UnityEngine;
//
// public class MainPlayerMovementScript : MonoBehaviour
// {
//     [Header("Movement Settings")]
//     public float sprintSpeed = 10f;
//     public float horizontalMovementMultiplier = 0.5f;
//     public float mouseSensitivity = 3f;
//     public float movementThreshold = 0.05f;
//
//     [Header("Camera Reference")]
//     public Camera mainCamera;
//
//     private CharacterController controller;
//     private Animator animator;
//     private float currentRotationX = 0f;
//
//     // Global flag to lock movement
//     public static bool dialouge = false;
//
//     void Start()
//     {
//         controller = GetComponent<CharacterController>();
//         animator = GetComponentInChildren<Animator>();
//
//         // Ensure we have a camera
//         if (mainCamera == null)
//         {
//             mainCamera = Camera.main;
//         }
//
//         // Initially lock the cursor (FPS style)
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;
//     }
//
//     void Update()
//     {
//         // If dialogue is active, disable movement & show cursor
//         if (dialouge)
//         {
//             Cursor.lockState = CursorLockMode.None;
//             Cursor.visible = true;
//
//             // Optionally tell the animator we are not moving
//             if (animator != null)
//             {
//                 animator.SetBool("IsSprinting", false);
//             }
//
//             // Skip the rest of movement logic
//             return;
//         }
//         else
//         {
//             // Normal locked-cursor mode
//             Cursor.lockState = CursorLockMode.Locked;
//             Cursor.visible = false;
//         }
//
//         // ----- Movement & Rotation Logic (only runs if dialouge == false) -----
//
//         // Horizontal rotation
//         float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//         transform.Rotate(Vector3.up * mouseX);
//
//         // Basic WASD input
//         float horizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > movementThreshold 
//             ? Input.GetAxisRaw("Horizontal") 
//             : 0f;
//         float vertical = Mathf.Abs(Input.GetAxisRaw("Vertical")) > movementThreshold 
//             ? Input.GetAxisRaw("Vertical") 
//             : 0f;
//
//         Vector3 moveDirection = transform.right * (horizontal * horizontalMovementMultiplier)
//                               + transform.forward * vertical;
//         moveDirection = moveDirection.normalized;
//
//         bool isMoving = moveDirection.magnitude > 0;
//
//         // Update animator
//         if (animator != null)
//         {
//             animator.SetBool("IsSprinting", isMoving);
//         }
//
//         // Move character
//         if (isMoving)
//         {
//             controller.Move(moveDirection * sprintSpeed * Time.deltaTime);
//         }
//
//         // Update camera position
//         UpdateCameraPosition();
//     }
//
//     void UpdateCameraPosition()
//     {
//         if (mainCamera == null) return;
//
//         // Example: a simple 3rd-person camera
//         Vector3 targetPosition = transform.position 
//             + transform.forward * -5f 
//             + Vector3.up * 2f;
//
//         mainCamera.transform.position = targetPosition;
//         mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
//     }
//
//     void OnApplicationFocus(bool hasFocus)
//     {
//         // If the application loses focus, unlock the cursor
//         if (!hasFocus)
//         {
//             Cursor.lockState = CursorLockMode.None;
//             Cursor.visible = true;
//         }
//     }
// }
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
            animator.SetBool("IsSprinting", isMoving);
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

        // A simple 3rd-person camera
        Vector3 targetPosition = transform.position
            + transform.forward * -5f
            + Vector3.up * 2f;

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
