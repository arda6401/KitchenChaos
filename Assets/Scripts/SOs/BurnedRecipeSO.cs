using UnityEngine;


[CreateAssetMenu(fileName = "BurnedRecipe")]
public class burnedRecipeSO : ScriptableObject
{
   public KitchenObjectSO input;
   public KitchenObjectSO output;
   public float fryingTimeMax;
}
