using TMPro;
using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private GameObject MenuGameObject;
    [SerializeField] private TextMeshProUGUI VolumeText;
    [SerializeField] private TextMeshProUGUI MusicText;



    [Header("Texts...")]
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveRightText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI InteractAlternativeText;
    [SerializeField] private TextMeshProUGUI PauseText;

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


        MoveUpText.text = GameInputHandler.Instance.GetBindingText(GameInputHandler.Binding.Move_Up);
        MoveDownText.text = GameInputHandler.Instance.GetBindingText(GameInputHandler.Binding.Move_Down);
        MoveLeftText.text = GameInputHandler.Instance.GetBindingText(GameInputHandler.Binding.Move_Left);
        MoveRightText.text = GameInputHandler.Instance.GetBindingText(GameInputHandler.Binding.Move_Right);
        InteractText.text = GameInputHandler.Instance.GetBindingText(GameInputHandler.Binding.Interact);
        InteractAlternativeText.text = GameInputHandler.Instance.GetBindingText(GameInputHandler.Binding.InteractAlternative);
        PauseText.text = GameInputHandler.Instance.GetBindingText(GameInputHandler.Binding.Pause);
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

    public void KeyBindButton(GameInputHandler.Binding binding)
    {
        SetBindingText(binding, "...");
        GameInputHandler.Instance.RebindBinding(binding, UpdateVisuals);
    }

    private void SetBindingText(GameInputHandler.Binding binding, string text)
    {
        switch (binding)
        {
            case GameInputHandler.Binding.Move_Up:
                MoveUpText.text = text;
                break;
            case GameInputHandler.Binding.Move_Down:
                MoveDownText.text = text;
                break;
            case GameInputHandler.Binding.Move_Left:
                MoveLeftText.text = text;
                break;
            case GameInputHandler.Binding.Move_Right:
                MoveRightText.text = text;
                break;
            case GameInputHandler.Binding.Interact:
                InteractText.text = text;
                break;
            case GameInputHandler.Binding.InteractAlternative:
                InteractAlternativeText.text = text;
                break;
            case GameInputHandler.Binding.Pause:
                PauseText.text = text;
                break;
        }
    }

    public void RebindMoveUpButton() => KeyBindButton(GameInputHandler.Binding.Move_Up);
    public void RebindMoveDownButton() => KeyBindButton(GameInputHandler.Binding.Move_Down);
    public void RebindMoveLeftButton() => KeyBindButton(GameInputHandler.Binding.Move_Left);
    public void RebindMoveRightButton() => KeyBindButton(GameInputHandler.Binding.Move_Right);
    public void RebindInteractButton() => KeyBindButton(GameInputHandler.Binding.Interact);
    public void RebindInteractAlternativeButton() => KeyBindButton(GameInputHandler.Binding.InteractAlternative);
    public void RebindPauseButton() => KeyBindButton(GameInputHandler.Binding.Pause);
}
