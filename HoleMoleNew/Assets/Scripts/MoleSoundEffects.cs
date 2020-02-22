using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MoleSoundEffects : MonoBehaviour
{
    [Header("Will override all volumes (not if 0)")]
    public float masterVolume;

    [Header("Shared Sound effects")]
    public AudioClip[] hitSounds;
    public float volume1;
    public AudioClip[] waterDeadSounds;
    public float volume2;
    public AudioClip[] normalDigSounds;
    public float volume3;

    [Header("Normal mole Sound effects")]
    public AudioClip[] normalDeadSounds;
    public float volume4;
    public AudioClip[] normalAppearSounds;
    public float volume5;
    public AudioClip[] plankBlockSounds;
    public float volume6;

    [Header("Elite mole Sound effects")]
    public AudioClip[] helmetHitSounds;
    public float volume7;
    public AudioClip[] eliteDeadSounds;
    public float volume8;
    public AudioClip[] eliteDigSounds;
    public float volume9;
    public AudioClip[] eliteAppearSounds;
    public float volume10;
    public AudioClip[] plankBreakSounds;
    public float volume11;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (masterVolume != 0)
        {
            volume1 = masterVolume;
            volume2 = masterVolume;
            volume3 = masterVolume;
            volume4 = masterVolume;
            volume5 = masterVolume;
            volume6 = masterVolume;
            volume7 = masterVolume;
            volume8 = masterVolume;
            volume9 = masterVolume;
            volume10 = masterVolume;
            volume11 = masterVolume;
        }
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
                audioSource.PlayOneShot(GetRandomSound(normalDeadSounds), volume4);
                break;
            case "Elite":
                audioSource.PlayOneShot(GetRandomSound(eliteDeadSounds), volume8);
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
                audioSource.PlayOneShot(GetRandomSound(normalAppearSounds), volume5);
                break;
            case "Elite":
                audioSource.PlayOneShot(GetRandomSound(eliteAppearSounds), volume10);
                break;
            default:
                break;
        }
    }

    public void PlayDigSound()
    {
        audioSource.PlayOneShot(GetRandomSound(normalDigSounds), volume3);
    }

    public void PlaySuperDigSound()
    {
        audioSource.PlayOneShot(GetRandomSound(eliteDigSounds), volume9);
    }

    public void PlayNormalhitSound()
    {
        audioSource.PlayOneShot(GetRandomSound(hitSounds), volume1);
    }

    public void PlayWaterDeadSound()
    {
        audioSource.PlayOneShot(GetRandomSound(waterDeadSounds), volume2);
    }

    public void PlayHelmethitSound()
    {
        audioSource.PlayOneShot(GetRandomSound(helmetHitSounds), volume7);
    }

    public void PlayPlankBreakSound()
    {
        audioSource.PlayOneShot(GetRandomSound(plankBreakSounds), volume11);
    }

    public void PlayPlankBlockSound()
    {
        audioSource.PlayOneShot(GetRandomSound(plankBlockSounds), volume6);
    }

}
