using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.GameReady);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState {
    GameReady,
    GameRunning,
    GameOver,
}
