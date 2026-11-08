using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public Action OnRecipeSpawned;
    public Action OnRecipeCompleted;
    public Action OnRecipeSuccessed;
    public Action OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeLists;
    private List<RecipeSO> waitingRecipeList;
    private float spawnRecipeTimer = 0f;
    private float spawnRecipTimerMax = 4f;
    private float waitingRecipeMax = 4f;

    private int DeliveredRecipeCount = 0;

    public int GetDeliveredRecipeCount()
    {
        return DeliveredRecipeCount;
    }




    private void Awake()
    {
        waitingRecipeList = new List<RecipeSO>();
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer < 0f)
        {
            spawnRecipeTimer = spawnRecipTimerMax;
            if (waitingRecipeList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipe = recipeLists.RecipeSOList[UnityEngine.Random.Range(0, recipeLists.RecipeSOList.Count)];
                waitingRecipeList.Add(waitingRecipe);
                Debug.Log(waitingRecipe.name);
                OnRecipeSpawned?.Invoke();
            }
        }

    }



    // todo: plate.containes() diye kontrol et / sadeleşştir...
    public void DeliverRecipe(PlateKitchenObject plate)
    {
        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSO recipe = waitingRecipeList[i];

            if (recipe.kitchenObjectSOList.Count != plate.GetKitchenObjectSOList().Count)
            {
                continue;
            }

            bool plateContentMatchesRecipe = true;
            // has same number

            foreach (KitchenObjectSO kitchenObject in recipe.kitchenObjectSOList)
            {

                bool ingredientFound = false;
                foreach (KitchenObjectSO plateKitchenObjectSO in plate.GetKitchenObjectSOList())
                {
                    if (plateKitchenObjectSO == kitchenObject)
                    {
                        // matches
                        ingredientFound = true;
                        break;
                    }
                }
                if (!ingredientFound)
                {
                    plateContentMatchesRecipe = false;
                }
            }

            if (plateContentMatchesRecipe)
            {
                // player correct
                waitingRecipeList.Remove(recipe);
                OnRecipeCompleted?.Invoke();
                OnRecipeSuccessed?.Invoke();
                DeliveredRecipeCount++;

                return;
            }

        }
        //  failed...
        OnRecipeFailed?.Invoke();

    }


    public List<RecipeSO> GetWaitingRecipes()
    {
        return waitingRecipeList;
    }

}
