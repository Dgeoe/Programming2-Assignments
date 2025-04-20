using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveTest : MonoBehaviour
{
    public float dissolveDuration = 10;
    public float dissolveStrength;

    public Color startColor;
    public Color endColor;

    public GameObject[] StartHub;


    public void StartDissolver()
    {
        foreach (GameObject child in StartHub)
        {
            StartCoroutine(DissolveOut(child));
        }
    }

    public IEnumerator DissolveIn()
    {
        float elapsedTime = 0;
        Material dissolveMaterial = GetComponent<Renderer>().material;
        Color lerpedColor;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(1, 0, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);

            lerpedColor = Color.Lerp(startColor, endColor, dissolveStrength);
            dissolveMaterial.SetColor("BaseColor", lerpedColor);

            yield return null;
        }
    }
    public IEnumerator DissolveOut(GameObject children)
    {
        float elapsedTime = 0;
        Material dissolveMaterial = children.GetComponent<Renderer>().material;
        Color lerpedColor;

        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(0, 1, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);

            lerpedColor = Color.Lerp(startColor, endColor, dissolveStrength);
            dissolveMaterial.SetColor("BaseColor", lerpedColor);

            yield return null;
        }
    }
}

