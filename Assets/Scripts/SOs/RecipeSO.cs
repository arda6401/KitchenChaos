using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Recipe")]
public class RecipeSO : ScriptableObject
{
  public List<KitchenObjectSO> kitchenObjectSOList;
  public string recipeName;

} 
