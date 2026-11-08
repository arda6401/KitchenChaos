using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    protected KitchenObject kitchenObject;
    [SerializeField] private Transform spawnPoint;

    public static event Action OnDropped;

    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter Interact");
    }
    public virtual void InteractAlternative(Player player)
    {
        Debug.Log("BaseCounter Interact Alternative");
    }

     public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        OnDropped?.Invoke();
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
        return spawnPoint;
    }

}
