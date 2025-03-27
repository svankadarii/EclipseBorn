using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform player;               // Drag the player GameObject here
    public float interactDistance = 10f;    // Increased distance from 3 to 6

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