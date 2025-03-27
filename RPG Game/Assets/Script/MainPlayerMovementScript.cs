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
    
    controller = GetComponent<CharacterController>();
    animator = GetComponentInChildren<Animator>();

    
    if (mainCamera == null)
    {
        mainCamera = Camera.main;
    }

    
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

void Update()
{
    
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    transform.Rotate(Vector3.up * mouseX);

    
    float horizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > movementThreshold 
        ? Input.GetAxisRaw("Horizontal") 
        : 0f;
    float vertical = Mathf.Abs(Input.GetAxisRaw("Vertical")) > movementThreshold 
        ? Input.GetAxisRaw("Vertical") 
        : 0f;

    
    Vector3 moveDirection = transform.right * (horizontal * horizontalMovementMultiplier) + 
                             transform.forward * vertical;
    moveDirection = moveDirection.normalized;

    
    bool isMoving = moveDirection.magnitude > 0;

    
    if (animator != null)
    {
        
        
        
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsSprinting", vertical>0);
        animator.SetBool("MovingB", vertical < 0);
        animator.SetBool("MovingR", horizontal < 0);
        animator.SetBool("MovingL", horizontal > 0);
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

    
    Vector3 targetPosition = transform.position + transform.forward * -5f + Vector3.up * 2f;

    
    mainCamera.transform.position = transform.position + transform.forward * -5f + Vector3.up * 2f;

    
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