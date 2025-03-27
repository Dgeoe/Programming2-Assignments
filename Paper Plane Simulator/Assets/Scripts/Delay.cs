using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Delay : MonoBehaviour
{
    public float delay;
    public string sceneName;

    void Start()
    {
        if (delay > 0 && sceneName != null)
        {
            StartCoroutine(PlayBall(delay, sceneName));  
        }
    }

    private IEnumerator PlayBall(float time, string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
