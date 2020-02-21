using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MoleSoundEffects : MonoBehaviour
{
    [Header("Shared Sound effects")]
    public AudioClip[] hitSounds;
    public AudioClip[] waterDeadSounds;
    public AudioClip[] normalDigSounds;

    [Header("Normal mole Sound effects")]
    public AudioClip[] normalDeadSounds;
    public AudioClip[] normalAppearSounds;
    public AudioClip[] plankBlockSounds;

    [Header("Elite mole Sound effects")]
    public AudioClip[] helmetHitSounds;
    public AudioClip[] eliteDeadSounds;
    public AudioClip[] eliteDigSounds;
    public AudioClip[] eliteAppearSounds;
    public AudioClip[] plankBreakSounds;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public AudioClip GetRandomSound(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }

    public void PlayDeadSound(string moleType)
    {
        switch (moleType)
        {
            case "Normal":
                audioSource.PlayOneShot(GetRandomSound(normalDeadSounds));
                break;
            case "Elite":
                audioSource.PlayOneShot(GetRandomSound(eliteDeadSounds));
                break;
            default:
                break;
        }
    }

    public void PlayAppearSound(string moleType)
    {
        switch (moleType)
        {
            case "Normal":
                audioSource.PlayOneShot(GetRandomSound(normalAppearSounds));
                break;
            case "Elite":
                audioSource.PlayOneShot(GetRandomSound(eliteAppearSounds));
                break;
            default:
                break;
        }
    }

    public void PlayDigSound()
    {
        audioSource.PlayOneShot(GetRandomSound(normalDigSounds));
    }

    public void PlaySuperDigSound()
    {
        audioSource.PlayOneShot(GetRandomSound(eliteDigSounds));
    }

    public void PlayNormalhitSound()
    {
        audioSource.PlayOneShot(GetRandomSound(hitSounds));
    }

    public void PlayWaterDeadSound()
    {
        audioSource.PlayOneShot(GetRandomSound(waterDeadSounds));
    }

    public void PlayHelmethitSound()
    {
        audioSource.PlayOneShot(GetRandomSound(helmetHitSounds));
    }

    public void PlayPlankBreakSound()
    {
        audioSource.PlayOneShot(GetRandomSound(plankBreakSounds));
    }

    public void PlayPlankBlockSound()
    {
        audioSource.PlayOneShot(GetRandomSound(plankBlockSounds));
    }

}
