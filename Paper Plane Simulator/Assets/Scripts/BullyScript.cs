using UnityEngine;

public class BullyScript : MonoBehaviour
{
    [Header("Idle")]
    private Animator animator;
    private float idleTime = 0f;
    private bool isYawning = false;

    public float yawnThreshold = 10f; // Time before yawning

    [Header("Chase")]
    public Transform player; 
    public float chaseDistance = 20f; // Detection range
    private float velocity = 0f;
    private float acceleration = 0.2f;
    private float deceleration = 0.3f;
    public float rotationSpeed = 5f; 

    [Header("Jump Scare")]
    public float jumpDistance = 8f; 
    private bool isJumping = false; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleIdleState();
        HandleChaseState();
    }

    private void HandleIdleState()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Happy Idle"))
        {
            idleTime += Time.deltaTime;

            if (idleTime >= yawnThreshold && !isYawning)
            {
                StartCoroutine(TriggerYawn());
            }
        }
        else
        {
            idleTime = 0f;
        }
    }

    private void HandleChaseState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= jumpDistance && !isJumping)
        {
            StartCoroutine(TriggerJumpScare());
            return; 
        }

        if (distanceToPlayer <= chaseDistance && !isJumping)
        {
            // Player entered chase range
            animator.SetBool("Enter", true);
            animator.SetBool("Exit", false);

            velocity += Time.deltaTime * acceleration;
            velocity = Mathf.Clamp(velocity, 0f, 1f); // Keep velocity between 0 and 1

            RotateTowardsPlayer(); // Rotate while chasing
        }
        else
        {
            // Player left chase range
            velocity -= Time.deltaTime * deceleration;
            velocity = Mathf.Max(velocity, 0f); // Ensure velocity never goes below 0

            if (velocity == 0)
            {
                animator.SetBool("Enter", false);
                animator.SetBool("Exit", true);
            }
        }

        animator.SetFloat("Velocity", velocity);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Prevent tilting hopefully ughhhhhhhhhh

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private System.Collections.IEnumerator TriggerYawn()
    {
        isYawning = true;
        animator.SetBool("Yawn", true);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("Yawn", false);
        isYawning = false;
        idleTime = 0f; 
    }

    private System.Collections.IEnumerator TriggerJumpScare()
    {
        isJumping = true;
        animator.SetBool("Enter 2", true);

        float jumpDuration = GetAnimationClipLength("Jump"); 
        
        if (jumpDuration <= 0)
            jumpDuration = 1.5f; 

        yield return new WaitForSeconds(jumpDuration);

        animator.SetBool("Enter 2", false);
        isJumping = false;
    }

    private float GetAnimationClipLength(string clipName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        return 0f; 
    }
}
