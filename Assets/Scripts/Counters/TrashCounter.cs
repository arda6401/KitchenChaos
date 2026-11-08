using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public static event Action<TrashCounter> OnTrashed;
    public override void Interact(Player player)
    {

        if (player.HasKitchenObject())
        {

            player.GetKitchenObject().DestroySelf();
            player.ClearKitchenObject();
            OnTrashed?.Invoke(this);
        }

    }

    new public static void ResetStaticData()
    {
        OnTrashed = null;
    }
}


