using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayPlankSound : MonoBehaviour
{
    public AudioClip[] sound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(int clipNum)
    {
        float volume = 1;

        if (clipNum == 2)
            volume = .5f;

        audioSource.PlayOneShot(sound[clipNum], volume);
    }
}
