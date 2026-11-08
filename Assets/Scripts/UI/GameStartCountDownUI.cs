using System;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI countDownText;


    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    public void Update()
    {
       countDownText.text = GameManager.Instance.GetCountDownToStartTimer().ToString("#");
    }

    private void GameManager_OnStateChanged()
    {
        if (GameManager.Instance.IsCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
