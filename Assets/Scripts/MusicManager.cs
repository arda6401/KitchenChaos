using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private float volume = 0.3f;
    private const string PLAYER_PREFS_Music_VOLUME = "MusicVolume";
    private AudioSource audioSource;
    public static MusicManager Instance;
    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_Music_VOLUME, 0.3f);
        audioSource.volume = volume;
        DontDestroyOnLoad(gameObject);
    }

    public void VolumeChange()
    {
        volume += 0.1f;
        if (volume > 1.1f)
            volume = 0f;


        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_Music_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume * 10f;
    }
}
