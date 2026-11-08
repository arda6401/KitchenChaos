using System;
using UnityEngine;

public class GameInputHandler : MonoBehaviour
{
    private PlayerInputActions playerInputs;
    public  event Action OnInteractionAction;
    public event Action OnInteractAlternativeAction;


    void Awake()
    {
        playerInputs= new PlayerInputActions();

      playerInputs.Player.Enable();

      playerInputs.Player.Interaction.performed += Interaction_performed;
      playerInputs.Player.InteractAlternative.performed += InteractAlternative_performed;
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
