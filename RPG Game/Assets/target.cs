using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (distance <= interactDistance)
            {
                animator.SetTrigger("doorMotion");
            }
        }
    }
}