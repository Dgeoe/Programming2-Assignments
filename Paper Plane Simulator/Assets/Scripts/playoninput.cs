using UnityEngine;

public class playoninput : MonoBehaviour
{
    private Animator animator;
    public GameObject IMAGINE;
    public GameObject CAm;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject!", this);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown) 
        {
            animator.SetTrigger("Click");
            IMAGINE.SetActive(true);
            CAm.SetActive(true);
        }
    }
}

