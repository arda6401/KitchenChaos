using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
  
  
    public event Action<ContainerCounter> OnPlayerGrabbedObject;  // for open door animation
    public KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
             KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this);
        }




    }




}
