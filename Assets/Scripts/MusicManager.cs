using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private float volume = 0.3f;
    private AudioSource audioSource;
    public static MusicManager Instance;
    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void VolumeChange()
    {
        volume += 0.1f;
        if (volume > 1.1f)
            volume = 0f;


            audioSource.volume = volume;
    }
    public float GetVolume()
    {
        return volume * 10f;
    }
}
