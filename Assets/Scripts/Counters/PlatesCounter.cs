using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public Action<PlatesCounter> OnPlateSpawned;
    public Action<PlatesCounter> OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    [SerializeField] private float spawnPlateMaxTimer = 4;
    private float spawnPlateTimer = 4;
    private int SpawnedPlateAmount = 0;
    private int SpawnedPlateMaxAmount = 4;


    void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateMaxTimer)
        {
            spawnPlateTimer = 0f;
            if (SpawnedPlateAmount < SpawnedPlateMaxAmount)
            {
                SpawnedPlateAmount++;
                OnPlateSpawned?.Invoke(this);
            }
        }
    }
    public override void Interact(Player player)
    {

        // tezgahın üstünde bişey yok
        if (!player.HasKitchenObject())
        {
            if (SpawnedPlateAmount > 0)
            {
                SpawnedPlateAmount--;
                OnPlateRemoved?.Invoke(this);
                KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                kitchenObject.SetKitchenObjectParent(player);
            }

        }
        else
        {
            // tezgahın üstünde bişey var
            if (!player.HasKitchenObject())
            {
                // playerda bişey yok
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }
}
