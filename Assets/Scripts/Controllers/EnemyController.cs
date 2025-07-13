using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private float spawnPositionY;
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerController.onPlayerAttack += PlayerController_onPlayerAttack;

        SetEnemyStartPosition();
    }

    // Update is called once per frame
    void Update() {
        HandleEnemyMovement();
        HandleEnemyAttack();

        DestroyIfOutOfScreen();
    }

    private void PlayerController_onPlayerAttack(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }

    private void SetEnemyStartPosition() {
        bool isSideSpawnLeft = Random.Range(0, 2) == 0;
        float spawnPositionX = isSideSpawnLeft ? enemySO.positionX * -1 : enemySO.positionX;

        transform.position = new Vector3(spawnPositionX, spawnPositionY, 0);
    }

    private void HandleEnemyMovement() {
        switch (enemySO.type) {
            case EnemySO.EnemyType.DOWN:
                MoveEnemyDown();
                break;
            case EnemySO.EnemyType.SIDEWAYS:
                break;
            case EnemySO.EnemyType.STATIC_SHOOT:
                break;
            case EnemySO.EnemyType.FLY:
                break;
        }
    }

    private void HandleEnemyAttack() {
        switch (enemySO.type) {
            case EnemySO.EnemyType.STATIC_SHOOT:
                break;
        }
    }

    private void MoveEnemyDown() {
        float enemySpeed = GameManager.Instance.GetClimbSpeed() + enemySO.speed;
        transform.position += new Vector3(0, -1 * enemySpeed * Time.deltaTime, 0);
    }

    private void DestroyIfOutOfScreen() {
        if (transform.position.y < -5.5f) {
            Destroy(gameObject);
        }
    }
}
