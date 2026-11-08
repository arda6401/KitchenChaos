using UnityEngine;

public class StoveSound : MonoBehaviour
{

    [SerializeField] StoveCounter stove;
    private AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stove.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(StoveCounter counter, StoveCounter.State state)
    {
        bool PlaySound = state == StoveCounter.State.Frying || state == StoveCounter.State.Fried;

        if (PlaySound)
        {
            audio.Play();
        }
        else
        {
            audio.Pause();
        }

    }



}
