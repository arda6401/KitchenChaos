using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event Action<CuttingCounter> OnCut;
     public static event Action<CuttingCounter> OnAnyCut;
    public event Action<float> OnProgressChanged;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSO;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // tezgahın üstünde bişey yok
            if (player.HasKitchenObject())
            {

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // playerda bişey var ve bu obje kesilebilir
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke((float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax);
                }
            }

        }
        else
        {
            // tezgahın üstünde bişey var
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                    OnProgressChanged?.Invoke(0);

                }


            }
            else
            {
                // playerda bişey yok
                GetKitchenObject().SetKitchenObjectParent(player);
                OnProgressChanged?.Invoke(0);
            }

        }
    }

    public override void InteractAlternative(Player player)
    {

        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //there is an object on the cutting board and it can be cut

            cuttingProgress++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());


            OnCut?.Invoke(this);
            OnAnyCut?.Invoke(this);
            OnProgressChanged?.Invoke((float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax);



            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                //cutting is done
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
            else
            {
                //cutting is not done yet
            }

        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);
        return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);
        return cuttingRecipeSO != null;
    }
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (var recipe in cuttingRecipeSO)
        {
            if (recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }
}
