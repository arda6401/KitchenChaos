using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject Container;
    [SerializeField] private GameObject Template;

    public void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted;
    }

    private void OnRecipeCompleted()
    {
        UpdateVisual();
    }

    private void OnRecipeSpawned()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in Container.transform)
        {
            if (child == Template) continue;

            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipe in DeliveryManager.Instance.GetWaitingRecipes())
        {
            var recipeUi = Instantiate(Template, Container.transform);
            recipeUi.GetComponent<RecipeUI>().Setup(recipe);

        }
    }


}
