using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputHandler : MonoBehaviour
{
  public static GameInputHandler Instance { get; private set; }
  private PlayerInputActions playerInputs;
  public event Action OnInteractionAction;
  public event Action OnInteractAlternativeAction;
  public event Action OnPauseAction;


  void Awake()
  {
     Instance = this;
    playerInputs = new PlayerInputActions();

    playerInputs.Player.Enable();

    playerInputs.Player.Interaction.performed += Interaction_performed;
    playerInputs.Player.InteractAlternative.performed += InteractAlternative_performed;
    playerInputs.Player.Pause.performed += Pause_performed;
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




}
