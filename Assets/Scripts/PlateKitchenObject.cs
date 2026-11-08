using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    public Action<KitchenObjectSO> AddedObject;
    private List<KitchenObjectSO> KitchenObjectSOList;
    [SerializeField] List<KitchenObjectSO> validKitchenObjectSOList;



    void Awake()
    {
        KitchenObjectSOList = new List<KitchenObjectSO>();
        
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // kullanılabilirse
            if (KitchenObjectSOList.Contains(kitchenObjectSO)) // already added
            {
                print($"{kitchenObjectSO.name} eşyası zatten tabağa eklenmiş!");
                return false;
            }

            else
            {
                KitchenObjectSOList.Add(kitchenObjectSO);
                AddedObject?.Invoke(kitchenObjectSO);
                return true;
            }
        }
        else
        {
            print($"{kitchenObjectSO.name} geçerli bir ürün değil!");
            return false;
        }

    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return KitchenObjectSOList;
    }
}
