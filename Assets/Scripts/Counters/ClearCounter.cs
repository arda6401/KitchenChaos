using UnityEngine;

public class ClearCounter : BaseCounter
{




    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // tezgahın üstünde bişey yok
            if (player.HasKitchenObject())
            {
                // playerda bişey var
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

        }
        else // tezgahın üstündde bişey var
        {
            if (player.HasKitchenObject()) // oyuncuda bişey varmı?
            {
                // oyuncunun elinde tabakmı var?
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();

                }
                else // elinde tabak yok...
                {

                    // masanin üstünde ttabak varmı?
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
                    {
                        plate.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO());
                        player.GetKitchenObject().DestroySelf();
                    }
                }

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);

            }

        }
    }
}
