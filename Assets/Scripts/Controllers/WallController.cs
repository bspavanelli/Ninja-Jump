using UnityEngine;

public class WallController : MonoBehaviour {
    private float distanceToReset = -9.6f;

    private float wallSpeed;

    private void Start() {
        GameManager.Instance.OnClimbSpeedChange += GameManager_OnClimbSpeedChange;
        
        wallSpeed = GameManager.Instance.GetClimbSpeed();
    }

    private void OnDestroy() {
        GameManager.Instance.OnClimbSpeedChange -= GameManager_OnClimbSpeedChange;
    }

    private void Update() {
        if (GameManager.Instance.GetGameState() == GameManager.State.GamePlaying) {
            if (transform.position.y <= distanceToReset) {
                transform.position = Vector3.zero;
            }
            transform.position += new Vector3(0, -wallSpeed * Time.deltaTime, 0);
        }
    }

    private void GameManager_OnClimbSpeedChange(object sender, System.EventArgs e) {
        wallSpeed = GameManager.Instance.GetClimbSpeed();
    }
}
