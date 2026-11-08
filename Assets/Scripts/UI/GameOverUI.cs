using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    
    [SerializeField] private TMPro.TextMeshProUGUI DeliveredRecipesText;
    [SerializeField] private GameObject gameOverUI;


    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

   

    private void GameManager_OnStateChanged()
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
              DeliveredRecipesText.text = DeliveryManager.Instance.GetDeliveredRecipeCount().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameOverUI.SetActive(false);
    }

    private void Show()
    {
        gameOverUI.SetActive(true);
    }
}
