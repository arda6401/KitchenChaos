using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputHandler : MonoBehaviour
{

  private const string PLAYER_PREFS_BINDINGS = "InputBindings";
  public static GameInputHandler Instance { get; private set; }
  private PlayerInputActions playerInputs;
  public event Action OnInteractionAction;
  public event Action OnInteractAlternativeAction;
  public event Action OnPauseAction;

[System.Serializable]
  public enum Binding
  {
    Move_Up,
    Move_Down,
    Move_Left,
    Move_Right,
    Interact,
    InteractAlternative,
    Pause
  }


  void Awake()
  {
    Instance = this;
    playerInputs = new PlayerInputActions();

    playerInputs.Player.Enable();

    playerInputs.Player.Interaction.performed += Interaction_performed;
    playerInputs.Player.InteractAlternative.performed += InteractAlternative_performed;
    playerInputs.Player.Pause.performed += Pause_performed;

    if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
    {
      playerInputs.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
    }
  }

  private void Pause_performed(InputAction.CallbackContext context)
  {
    OnPauseAction?.Invoke();
  }

  public Vector2 GetMovementVectorNormalized()
  {
    Vector2 inputVector = playerInputs.Player.Move.ReadValue<Vector2>();


    //  inputVector= inputVector.normalized;
    return inputVector;

  }
  private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    OnInteractionAction?.Invoke();
  }
  private void InteractAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    OnInteractAlternativeAction?.Invoke();
  }


  public string GetBindingText(Binding binding)
  {
    switch (binding)
    {
      default:
      case Binding.Move_Up:
        return playerInputs.Player.Move.bindings[1].ToDisplayString();
      case Binding.Move_Down:
        return playerInputs.Player.Move.bindings[2].ToDisplayString();
      case Binding.Move_Left:
        return playerInputs.Player.Move.bindings[3].ToDisplayString();
      case Binding.Move_Right:
        return playerInputs.Player.Move.bindings[4].ToDisplayString();
      case Binding.Interact:
        return playerInputs.Player.Interaction.bindings[0].ToDisplayString();
      case Binding.InteractAlternative:
        return playerInputs.Player.InteractAlternative.bindings[0].ToDisplayString();
      case Binding.Pause:
        return playerInputs.Player.Pause.bindings[0].ToDisplayString();
    }
  }

  public void RebindBinding(Binding binding, Action onActionRebound)
  {
    playerInputs.Player.Disable();

    InputAction inputAction;
    int bindingIndex;

    switch (binding)
    {
      default:
      case Binding.Move_Up:
        inputAction = playerInputs.Player.Move;
        bindingIndex = 1;
        break;
      case Binding.Move_Down:
        inputAction = playerInputs.Player.Move;
        bindingIndex = 2;
        break;
      case Binding.Move_Left:
        inputAction = playerInputs.Player.Move;
        bindingIndex = 3;
        break;
      case Binding.Move_Right:
        inputAction = playerInputs.Player.Move;
        bindingIndex = 4;
        break;
      case Binding.Interact:
        inputAction = playerInputs.Player.Interaction;
        bindingIndex = 0;
        break;
      case Binding.InteractAlternative:
        inputAction = playerInputs.Player.InteractAlternative;
        bindingIndex = 0;
        break;
      case Binding.Pause:
        inputAction = playerInputs.Player.Pause;
        bindingIndex = 0;
        break;

    }

    inputAction.PerformInteractiveRebinding(bindingIndex)
       .OnComplete(callback =>
       {
         callback.Dispose();
         playerInputs.Player.Enable();
         onActionRebound();

         PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, inputAction.SaveBindingOverridesAsJson());
         PlayerPrefs.Save();

       })
       .Start();
  }




}
