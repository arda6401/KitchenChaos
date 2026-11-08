using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    
    [SerializeField] private Image Icon;


    public void SetIcon(KitchenObjectSO kitchenObject)
    {
        Icon.sprite =  kitchenObject.sprite; 
    }
}
