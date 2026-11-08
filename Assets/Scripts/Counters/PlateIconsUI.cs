using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject kitchenObject;
    [SerializeField] private GameObject Icontemplate;


    void Start()
    {
        kitchenObject.AddedObject += OnAddedObject;
    }

    private void OnAddedObject(KitchenObjectSO kitchenObjectSO)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject == Icontemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO objectSO in kitchenObject.GetKitchenObjectSOList())
        {
            GameObject Icon = Instantiate(Icontemplate, transform);
            Icon.SetActive(true);
            Icon.GetComponent<PlateIconUI>().SetIcon(objectSO);

        }
    }
}
