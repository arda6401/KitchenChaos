using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject kitchenobjectGameobject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjects;


    void Awake()
    {

    }
    void Start()
    {
        plateKitchenObject.AddedObject += OnAddedObject;
    }

    private void OnAddedObject(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO_GameObject objectSO in KitchenObjects)
        {
            if (objectSO.kitchenObjectSO == kitchenObjectSO)
            {
                objectSO.kitchenobjectGameobject.SetActive(true);
            }
        }
    }
}
