using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{

    [SerializeField] private Image IconTemplate;
    [SerializeField] private Transform Container;

    [SerializeField] private TextMeshProUGUI recipeNameText;
    public void Setup(RecipeSO recipe)
    {
        recipeNameText.text = recipe.recipeName;

        foreach (KitchenObjectSO objectSO in recipe.kitchenObjectSOList)
        {
            Image Icon = Instantiate(IconTemplate, Container);
            Icon.sprite = objectSO.sprite;
            Icon.gameObject.SetActive(true);
        }
    }
}
