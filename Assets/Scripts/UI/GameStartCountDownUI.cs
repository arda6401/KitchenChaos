using System;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{

    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TMPro.TextMeshProUGUI countDownText;
    private Animator animator;
    private int previousCountDownTimer = 0;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    public void Update()
    {
        int countDownTimer = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
       countDownText.text = countDownTimer.ToString();
       if(countDownTimer != previousCountDownTimer)
        {
            previousCountDownTimer = countDownTimer;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountDownSound();
        }

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
