using UnityEngine;


[CreateAssetMenu(fileName = "KitchenObject")]
public class KitchenObjectSO : ScriptableObject
{
   public GameObject prefab;
   public Sprite sprite;
   public string objectName;
}
