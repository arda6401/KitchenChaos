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
    private float WaitingToStartTimer = 1f;
    private float CountDownToStartTimer = 3f;

    private float PlayingTimer = 10f;
     private bool isGamePaused = false;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInputHandler.Instance.OnPauseAction += GameInputHandler_OnPauseAction;
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

    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                WaitingToStartTimer -= Time.deltaTime;
                if (WaitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke();
                }
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
}
