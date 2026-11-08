using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }
    [SerializeField] private SoundSO Sounds;


    private float volume = 1f;
    private float MusicVolume = 1f;





    void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccessed += OnRecipeSuccessed;
        DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
        CuttingCounter.OnAnyCut += OnCuting;
        Player.Instance.OnPickupSomethings += OnPickup;
        BaseCounter.OnDropped += OnDropped;
        TrashCounter.OnTrashed += OnTrashed;
    }

    private void Awake()
    {
        Instance = this;
    }


    private void OnRecipeSuccessed()
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(Sounds.deliverySuccess, deliveryCounter.transform.position);
    }

    private void OnPickup()
    {

        PlaySound(Sounds.objectPickup, Player.Instance.gameObject.transform.position);
    }
    private void OnDropped()
    {

        PlaySound(Sounds.objectDrop, Player.Instance.gameObject.transform.position);
    }
    private void OnCuting(CuttingCounter cuttingCounter)
    {

        PlaySound(Sounds.chop, cuttingCounter.gameObject.transform.position);
    }
    private void OnTrashed(TrashCounter trashCounter)
    {

        PlaySound(Sounds.chop, trashCounter.gameObject.transform.position);
    }

    private void OnRecipeFailed()
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(Sounds.deliveryFail, deliveryCounter.transform.position);

    }
    private void PlaySound(AudioClip audio, Vector3 pos, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audio, pos, volume);
    }
    private void PlaySound(AudioClip[] audioclipArray, Vector3 pos, float volumeMixing = 1f)
    {
        PlaySound(audioclipArray[Random.Range(0, audioclipArray.Length)], pos, volume * volumeMixing);
    }
    public void PlayStepSound(Vector3 pos, float volume = 1f)
    {
        PlaySound(Sounds.footsteps, pos, volume);
    }

    public void VolumeChange()
    {
        volume += 0.1f;
        if (volume > 1.1f)
            volume = 0f;
    }
    public float GetVolume()
    {
        return volume * 10f;
    }
}
