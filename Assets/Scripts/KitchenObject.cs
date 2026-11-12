using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent KitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }


        this.KitchenObjectParent = kitchenObjectParent;


        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent zaten bir KitchenObject içeriyor");

        }
        kitchenObjectParent.SetKitchenObject(this);
        gameObject.transform.parent = kitchenObjectParent.GetSpawnPoint();
        gameObject.transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return KitchenObjectParent;
    }
    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject plate)
        {
            plateKitchenObject = plate;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }


       // sınıfın kendi kendine ait static bir metodu var. Bu metod, verilen KitchenObjectSO ve IKitchenObjectParent parametrelerini kullanarak yeni bir KitchenObject oluşturur ve onu belirtilen parent ile ilişkilendirir. Bu sayede, oyun içinde mutfak objeleri dinamik olarak oluşturulabilir ve yönetilebilir.
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        GameObject kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }


}
