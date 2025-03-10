using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TimedSfx : MonoBehaviour
{
    public AudioClip audioClip;
    [Range(0f, 1f)] public float playPercentage = 1.0f; 

    private AudioSource audioSource;
    private float playDuration;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            playDuration = audioClip.length * playPercentage; // Calculate the duration to play
            PlayClip();
        }
        else
        {
            Debug.LogWarning("No AudioClip assigned!", this);
        }
    }

    private void PlayClip()
    {
        audioSource.Play();
        Invoke(nameof(StopClip), playDuration); 
    }

    private void StopClip()
    {
        audioSource.Stop();
    }

    private void OnValidate()
    {
        if (audioClip != null)
        {
            playDuration = audioClip.length * playPercentage; // Update duration in real-time
        }
    }
}

