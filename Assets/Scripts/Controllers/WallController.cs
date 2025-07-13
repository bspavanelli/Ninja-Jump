using UnityEngine;

public class WallController : MonoBehaviour {
    private float distanceToReset = -9.6f;

    private float wallSpeed;

    private void Start() {
        GameManager.Instance.onClimbSpeedChange += GameManager_onClimbSpeedChange;
        
        wallSpeed = GameManager.Instance.GetClimbSpeed();
    }

    private void OnDestroy() {
        GameManager.Instance.onClimbSpeedChange -= GameManager_onClimbSpeedChange;
    }

    private void Update() {
        if (transform.position.y <= distanceToReset) {
            transform.position = Vector3.zero;
        }
        transform.position += new Vector3(0, -1 * wallSpeed * Time.deltaTime, 0);
    }

    private void GameManager_onClimbSpeedChange(object sender, System.EventArgs e) {
        wallSpeed = GameManager.Instance.GetClimbSpeed();
        Debug.Log("Alterado velocidade da parede para " + wallSpeed); 
    }
}
