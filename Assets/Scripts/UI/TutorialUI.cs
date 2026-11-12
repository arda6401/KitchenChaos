using System;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject TutorialMenuGameObject;



    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        if (IsNewPlayer())
        {
            Show();

        }
    }

    private void GameManager_OnStateChanged()
    {
           if (GameManager.Instance.IsCountDownToStartActive())
        {
            Hide();

        }
    }

    private bool IsNewPlayer()
    {
        return PlayerPrefs.GetInt("PlayerHasCompletedTutorial") == 0;
    }









    private void Hide()
    {
        TutorialMenuGameObject.SetActive(false);
    }

    private void Show()
    {
        TutorialMenuGameObject.SetActive(true);
    }
}
