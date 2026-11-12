using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    public static GameManager Instance { get; private set; }


    public event Action OnStateChanged;
    public event Action OnGamePaused;
    public event Action OnGameUnpaused;

    private State state;

    private float CountDownToStartTimer = 3f;

    private float PlayingTimer = 100f;
    private bool isGamePaused = false;


    private void Awake()
    {
        Instance = this;
        if (IsNewPlayer())
            state = State.WaitingToStart;
        else
        state= State.CountdownToStart;
    }

    private void Start()
    {
        GameInputHandler.Instance.OnPauseAction += GameInputHandler_OnPauseAction;
        GameInputHandler.Instance.OnInteractionAction += GameManager_OnInteract;
    }

    private void GameInputHandler_OnPauseAction()
    {

        TogglePauseGame();
    }



    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
        }
    }
    private void GameManager_OnInteract()
    {

       
        PlayerPrefs.SetInt("PlayerHasCompletedTutorial", 1);
        PlayerPrefs.Save();
        state = State.CountdownToStart;
        OnStateChanged?.Invoke();
        GameInputHandler.Instance.OnInteractionAction -= GameManager_OnInteract;
    }

    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:

                break;
            case State.CountdownToStart:

                CountDownToStartTimer -= Time.deltaTime;
                if (CountDownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke();
                }
                break;
            case State.GamePlaying:

                PlayingTimer -= Time.deltaTime;
                if (PlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke();
                }
                break;
            case State.GameOver:
                break;
        }
    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive()
    {
        return state == State.CountdownToStart;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }


    public float GetCountDownToStartTimer()
    {
        return CountDownToStartTimer;
    }
    private bool IsNewPlayer()
    {
        return PlayerPrefs.GetInt("PlayerHasCompletedTutorial") == 0;
    }


}
