using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum State {
        WaitingToStart,
        GamePlaying,
        GameOver,
    }

    public static GameManager Instance { get; private set; }

    public event EventHandler onClimbSpeedChange;
    public event EventHandler onGameStateChange;

    [SerializeField] private float startClimbingSpeed;

    [Header("Climbind Speed Increase Params")]
    [SerializeField] private float speedChangeInterval;
    [SerializeField] private float speedMultiplier;

    private float currentClimbSpeed;
    private State gameState;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one GameManager Instance");
        }
        Instance = this;
    }

    private void Start() {
        StartGame();

        StartCoroutine(HandleClimbSpeedIncreaseOverTime());
    }

    IEnumerator HandleClimbSpeedIncreaseOverTime() {
        while (gameState == State.GamePlaying) {
            Debug.Log("Aumentando a velocidade em " + speedMultiplier + "% da velocidade inicial");
            IncreaseClimbSpeed(speedMultiplier);

            yield return new WaitForSeconds(speedChangeInterval);
        }
    }

    private void StartGame() {
        currentClimbSpeed = startClimbingSpeed;
        gameState = State.GamePlaying;
        onGameStateChange?.Invoke(this, EventArgs.Empty);
    }

    public void GameOver() {
        currentClimbSpeed = 0;
        gameState = State.GameOver;
        onGameStateChange?.Invoke(this, EventArgs.Empty);
    }

    public float GetClimbSpeed() {
        return currentClimbSpeed;
    }

    private void IncreaseClimbSpeed(float multiplier) {
        currentClimbSpeed += (startClimbingSpeed * multiplier);
        onClimbSpeedChange?.Invoke(this, EventArgs.Empty);
        Debug.Log("Aumentado velocidade para " + currentClimbSpeed);
    }

    public State GetGameState() {
        return gameState;
    }
}
