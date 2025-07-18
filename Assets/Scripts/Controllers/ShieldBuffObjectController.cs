using UnityEngine;

public class ShieldBuffObjectController : MonoBehaviour {
    [SerializeField] private float spawnPositionX;
    [SerializeField] private float spawnPositionY;

    void Start() {
        SetShieldBuffObjectStartPosition();
    }

    void Update() {
        HandleShieldBuffObjectMovement();

        DestroyIfOutOfScreen();
    }
    private void SetShieldBuffObjectStartPosition() {
        bool isSideSpawnLeft = Random.Range(0, 2) == 0;
        spawnPositionX = isSideSpawnLeft ? -spawnPositionX : spawnPositionX;

        transform.position = new Vector3(spawnPositionX, spawnPositionY, 0);
    }

    private void HandleShieldBuffObjectMovement() {
        float enemySpeed = GameManager.Instance.GetClimbSpeed();
        transform.position += new Vector3(0, -enemySpeed * Time.deltaTime, 0);
    }
    private void DestroyIfOutOfScreen() {
        if (transform.position.y < -5.5f) {
            Destroy(gameObject);
        }
    }
}
