using UnityEngine;
using System.Collections;

public class Dissolver : MonoBehaviour
{
    private Material material; 
    public string propertyName = "DissolveStrength"; 
    public string animatorParent = "Canvas";
    public string animationName = "Upandfade";
    public float duration = 1.0f; 
    Animator targetAnimator; 

    private Coroutine currentCoroutine;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("No Renderer found on this GameObject!");
        }
        targetAnimator = GameObject.Find(animatorParent).GetComponent<Animator>();

    }

    void Update()
    {
        AnimatorStateInfo stateInfo = targetAnimator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1f)
        {
            Debug.Log("Check1");
            float animLength = targetAnimator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(WaitForAnimation(animLength));
        }
    }

    private IEnumerator WaitForAnimation(float waitTime)
    {
        Debug.Log("Check2");
        yield return new WaitForSeconds(waitTime);
        MoveToZero();
    }

    public void MoveToZero()
    {
        Debug.Log("check3");
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ChangeValue(1f, 0f));
    }

    public void MoveToOne()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ChangeValue(0f, 1f));
    }

    private IEnumerator ChangeValue(float start, float end)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float newValue = Mathf.Lerp(start, end, timeElapsed / duration);
            material.SetFloat(propertyName, newValue);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        material.SetFloat(propertyName, end);
    }
}


