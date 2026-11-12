using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    public event Action<StoveCounter, State> OnStateChanged;
    public event Action<float> OnProgressChanged;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOs;
    [SerializeField] private burnedRecipeSO[] burnedRecipeSOs;


    private float fryingProgress;
    private State state;
    private FryingRecipeSO fryingRecipeSO;
    private burnedRecipeSO burnedRecipeSO;

    void Start()
    {
        state = State.Idle;
    }
    void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    OnStateChanged?.Invoke(this, state);
                    break;
                case State.Frying:
                    fryingProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke((float)fryingProgress / fryingRecipeSO.fryingTimeMax);
                    if (fryingProgress >= fryingRecipeSO.fryingTimeMax)
                    {
                        //frying is done

                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        fryingProgress = 0;
                        burnedRecipeSO = GetBurnedRecipeSOWithInput(fryingRecipeSO.output);
                        if (burnedRecipeSO == null)
                        {
                            Debug.LogError($"No burned recipe found for fried item '{fryingRecipeSO.output.name}'. Add a BurnedRecipeSO with this item as its input.", this);
                        }
                        state = State.Fried;
                        OnStateChanged?.Invoke(this, state);
                    }
                    break;
                case State.Fried:
                    if (burnedRecipeSO == null)
                    {
                        break;
                    }

                    fryingProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke((float)fryingProgress / burnedRecipeSO.fryingTimeMax);

                    if (fryingProgress >= burnedRecipeSO.fryingTimeMax)
                    {
                        //burning is done
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burnedRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, state);
                    }
                    break;
                case State.Burned:
                    OnStateChanged?.Invoke(this, state);
                    break;
            }
        }
    }

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
                    fryingProgress = 0;

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    OnStateChanged?.Invoke(this, state);
                    OnProgressChanged?.Invoke(0);
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
                    {
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, state);
                        OnProgressChanged?.Invoke(0);
                        GetKitchenObject().DestroySelf();
                    }


                }

            }
            else
            {
                // playerda bişey yok
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, state);
                OnProgressChanged?.Invoke(0);
            }

        }
    }

    public override void InteractAlternative(Player player)
    {

        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //there is an object on the frying pan and it can be fried

            fryingProgress++;

            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());


            // OnCut?.Invoke(this);
            // OnFryingProgressChanged?.Invoke((float)fryingProgress / fryingRecipeSO.fryingTimeMax);



            if (fryingProgress >= fryingRecipeSO.fryingTimeMax)
            {
                //frying is done
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
            else
            {
                //frying is not done yet
            }

        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(input);
        return fryingRecipeSO != null ? fryingRecipeSO.output : null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(input);
        return fryingRecipeSO != null;
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (var recipe in fryingRecipeSOs)
        {
            if (recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }
    private burnedRecipeSO GetBurnedRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (var recipe in burnedRecipeSOs)
        {
            if (recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }
    public bool IsFried()
    {
        return state == State.Fried;
    }
}
