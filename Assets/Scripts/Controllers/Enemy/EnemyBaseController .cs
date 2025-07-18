using UnityEngine;

public abstract class EnemyBaseController : MonoBehaviour {
    [SerializeField] private float spawnPositionY;
    [SerializeField] protected EnemySO enemySO;
    
    private PlayerController playerController;
    protected bool isMoveDirectionLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerController = GameManager.Instance.GetPlayerController();

        playerController.OnPlayerAttack += PlayerController_OnPlayerAttack;

        SetEnemyStartPosition();
    }


    private void OnDestroy() {
        playerController.OnPlayerAttack -= PlayerController_OnPlayerAttack;
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.GetGameState() == GameManager.State.GamePlaying) {
            HandleEnemyMovement();
            HandleEnemyAttack();

            DestroyIfOutOfScreen();
        }
    }

    private void PlayerController_OnPlayerAttack(object sender, PlayerController.OnPlayerAttackEventArgs e) {
        if (e.gameObject == gameObject) {
            Destroy(gameObject);
        }
    }

    private void SetEnemyStartPosition() {
        bool isSideSpawnLeft = Random.Range(0, 2) == 0;
        float spawnPositionX = isSideSpawnLeft ? -enemySO.positionX : enemySO.positionX;

        isMoveDirectionLeft = !isSideSpawnLeft;
        transform.position = new Vector3(spawnPositionX, spawnPositionY, 0);
    }

    protected abstract void HandleEnemyMovement();

    protected abstract void HandleEnemyAttack();

    private void DestroyIfOutOfScreen() {
        if (transform.position.y < -5.5f) {
            Destroy(gameObject);
        }
    }
}
