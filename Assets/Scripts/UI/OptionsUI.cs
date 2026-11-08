using TMPro;
using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private GameObject MenuGameObject;
    [SerializeField] private TextMeshProUGUI VolumeText;
    [SerializeField] private TextMeshProUGUI MusicText;
    public static OptionsUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
      
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisuals();

    }


    public void VolumeButton()
    {
        SoundManager.Instance.VolumeChange();
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        VolumeText.text = "Ses: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        MusicText.text = "Müzik: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }
    public void MusicButton()
    {
        MusicManager.Instance.VolumeChange();
        UpdateVisuals();
    }


    private void GameManager_OnGameUnpaused()
    {
        Hide();
    }


    public void Hide()
    {
        MenuGameObject.SetActive(false);
    }

    public void Show()
    {
        MenuGameObject.SetActive(true);
    }
}
