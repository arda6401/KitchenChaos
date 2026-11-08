using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{


    // events
    public Action<BaseCounter> OnSelectedCounterChanged;
    public Action OnPickupSomethings;

    // siglettion
    public static Player Instance { get; private set; }


    [SerializeField] private GameInputHandler gameInput;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask interactionLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;




    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private Vector3 lastInteractDir;



    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    void Start()
    {
        // lastInteractDir = transform.forward;

        if (gameInput != null)
        {
            gameInput.OnInteractionAction += OnInteractionAction;
            gameInput.OnInteractAlternativeAction += OnInteractAlternativeAction;
        }
    }


    private void Update()
    {
        HandleMove();
        HandleInteraction();

    }

    private void OnInteractionAction()
    {
        //        print("Interaction action triggered");
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
    void OnInteractAlternativeAction()
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternative(this);
        }
    }

    private void HandleInteraction()
    {

        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(input.x, 0f, input.y).normalized;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactionDistance = 2f; // ışının gideceği mesafe (etkileşim mesafesi)
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactionDistance, interactionLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

        //  Debug.Log(selectedCounter);

    }

    private void HandleMove()
    {
        if (gameInput == null)
        {
            return;
        }

        Vector2 input = gameInput.GetMovementVectorNormalized();

        if (input == Vector2.zero)
        {
            return;
        }



        Vector3 moveDir = new Vector3(input.x, 0f, input.y).normalized;


        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.5f;
        float playerHeight = 2f;
        bool CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!CanMove)
        {
            // only move X
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            CanMove = moveDirX.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (CanMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // only move Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (CanMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    // cannot move in any direction
                    return;
                }
            }
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float rotationSpeed = 5f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(selectedCounter);
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        OnPickupSomethings?.Invoke();
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetSpawnPoint()
    {
        return KitchenObjectHoldPoint;
    }
}
