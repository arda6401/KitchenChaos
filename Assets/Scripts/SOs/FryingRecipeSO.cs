using UnityEngine;


[CreateAssetMenu(fileName = "FryingRecipe")]
public class FryingRecipeSO : ScriptableObject
{
   public KitchenObjectSO input;
   public KitchenObjectSO output;
   public float fryingTimeMax;
}
