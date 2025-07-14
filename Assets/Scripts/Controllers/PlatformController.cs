using UnityEngine;

public class PlatformController : MonoBehaviour {
    [SerializeField] private float spawnPositionY;

    private float platformSpeed;

    private void Start() {
        GameManager.Instance.OnClimbSpeedChange += GameManager_OnClimbSpeedChange;

        platformSpeed = GameManager.Instance.GetClimbSpeed();

        transform.position = new Vector3(0, spawnPositionY, 0);
    }

    private void OnDestroy() {
        GameManager.Instance.OnClimbSpeedChange -= GameManager_OnClimbSpeedChange;
    }

    private void Update() {
        if (GameManager.Instance.GetGameState() == GameManager.State.GamePlaying) {
            transform.position += new Vector3(0, -platformSpeed * Time.deltaTime, 0);
        }

        DestroyIfOutOfScreen();
    }

    private void GameManager_OnClimbSpeedChange(object sender, System.EventArgs e) {
        platformSpeed = GameManager.Instance.GetClimbSpeed();
    }

    private void DestroyIfOutOfScreen() {
        if (transform.position.y < -5.5f) {
            Destroy(gameObject);
        }
    }
}
