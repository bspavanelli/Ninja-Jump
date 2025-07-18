using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum State {
        WaitingToStart,
        GamePlaying,
        GameOver,
    }

    public static GameManager Instance { get; private set; }

    public event EventHandler OnClimbSpeedChange;
    public event EventHandler OnGameStateChange;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private float startClimbingSpeed;

    [Header("Climbind Speed Increase Params")]
    [SerializeField] private float speedChangeInterval;
    [SerializeField] private float speedToAdd;

    private float currentClimbSpeed;
    private State gameState;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one GameManager Instance");
        }
        Instance = this;
    }

    IEnumerator HandleClimbSpeedIncreaseOverTime() {
        yield return new WaitForSeconds(speedChangeInterval);
        
        while (gameState == State.GamePlaying) {
            IncreaseClimbSpeed(speedToAdd);

            yield return new WaitForSeconds(speedChangeInterval);
        }
    }

    public void StartGame() {
        SetClimbSpeed(startClimbingSpeed);

        SetGameState(State.GamePlaying);

        StartCoroutine(HandleClimbSpeedIncreaseOverTime());
    }

    public void GameOver() {
        SetGameState(State.GameOver);
    }

    public float GetClimbSpeed() {
        return currentClimbSpeed;
    }

    private void SetClimbSpeed(float climbSpeed) {
        currentClimbSpeed = climbSpeed;
        OnClimbSpeedChange?.Invoke(this, EventArgs.Empty);
    }

    private void IncreaseClimbSpeed(float speedToAdd) {
        currentClimbSpeed += speedToAdd;
        OnClimbSpeedChange?.Invoke(this, EventArgs.Empty);
    }

    public State GetGameState() {
        return gameState;         
    }

    public void SetGameState(State state) {
        gameState = state;
        OnGameStateChange?.Invoke(this, EventArgs.Empty);
    }

    public PlayerController GetPlayerController() {
        return playerController;
    }
}
