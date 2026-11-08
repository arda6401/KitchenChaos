using UnityEngine;


[CreateAssetMenu(fileName = "CuttingRecipe")]
public class CuttingRecipeSO : ScriptableObject
{
   public KitchenObjectSO input;
   public KitchenObjectSO output;
   public int cuttingProgressMax;
}
