using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameOverUI;

    private void Start() {
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;

        startUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    private void OnDestroy() {
        GameManager.Instance.OnGameStateChange -= GameManager_OnGameStateChange;
    }

    private void GameManager_OnGameStateChange(object sender, System.EventArgs e) {
        switch (GameManager.Instance.GetGameState()) {
            case GameManager.State.WaitingToStart:
                startUI.SetActive(true);
                gameOverUI.SetActive(false);
                break;
            case GameManager.State.GamePlaying:
                startUI.SetActive(false);
                break;
            case GameManager.State.GameOver:
                gameOverUI.SetActive(true);
                break;
        }
    }

    public void OnRetryButtonClick() {
        SceneManager.LoadScene("SampleScene");
    }
}
