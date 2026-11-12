
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DeliveredUI : MonoBehaviour
{
    [SerializeField] private GameObject deliveredGameObject;
    [SerializeField] private Color ColorSuccess;
    [SerializeField] private Color ColorFailed;
    [SerializeField] private TextMeshProUGUI deliveredText;
    [SerializeField] private Sprite SuccessSprite;
    [SerializeField] private Sprite FailedSprite;
    [SerializeField] private Image deliveredImage;
    [SerializeField] private Image deliveredIcon;
    [SerializeField] private float XPosition = 2f;


    private float showRecipeSuccessTimer = 2f;
    void Start()
    {


        DeliveryManager.Instance.OnRecipeSuccessed += DeliveryManager_OnRecipeSuccessed;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeFailed()
    {
        deliveredGameObject.SetActive(true);
        deliveredText.text = "Teslimat\nBaşarısız!";
        deliveredImage.color = ColorFailed;
        deliveredIcon.sprite = FailedSprite;

        RectTransform rectTransform = deliveredGameObject.GetComponent<RectTransform>();


        rectTransform.DOKill();

        // Başlangıç X pozisyonunu sakla (sağa geri dönmek için)
        float startX = rectTransform.anchoredPosition.x;

        // 1) Sola belirli bir X'e kay (örn. -300f)
        rectTransform.DOAnchorPosX(XPosition, 0.5f).SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            // 2) Bekle, sonra sağa geri dön
            DOVirtual.DelayedCall(showRecipeSuccessTimer, () =>
            {
                rectTransform.DOAnchorPosX(startX, 0.5f).SetEase(Ease.InBack);
            });
        });






    }

    private void DeliveryManager_OnRecipeSuccessed()
    {

        deliveredText.text = "Teslimat\nBaşarılı!";
        deliveredImage.color = ColorSuccess;
        deliveredIcon.sprite = SuccessSprite;

        RectTransform rectTransform = deliveredGameObject.GetComponent<RectTransform>();


        rectTransform.DOKill();

        // Başlangıç X pozisyonunu sakla (sağa geri dönmek için)
        float startX = rectTransform.anchoredPosition.x;

        // 1) Sola belirli bir X'e kay (örn. -300f)
        rectTransform.DOAnchorPosX(XPosition, 0.5f).SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            // 2) Bekle, sonra sağa geri dön
            DOVirtual.DelayedCall(showRecipeSuccessTimer, () =>
            {
                rectTransform.DOAnchorPosX(startX, 0.5f).SetEase(Ease.InBack);
                deliveredGameObject.SetActive(false);
            });
        });


    }
}


