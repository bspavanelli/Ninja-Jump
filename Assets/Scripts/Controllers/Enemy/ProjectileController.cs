using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ProjectileController : MonoBehaviour {
    [SerializeField] private ProjectileSO projectileSO;

    private PlayerController playerController;
    protected bool isMoveDirectionLeft;
    private float projectileSpeedX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerController = GameManager.Instance.GetPlayerController();

        playerController.OnPlayerAttack += PlayerController_OnPlayerAttack;

        SetMoveDirection();
    }

    private void OnDestroy() {
        playerController.OnPlayerAttack -= PlayerController_OnPlayerAttack;
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.GetGameState() == GameManager.State.GamePlaying) {
            HandleProjectileVerticalMovement();
            HandleProjectileHorizontalMovement();

            DestroyIfOutOfScreen();
        }
    }

    private void PlayerController_OnPlayerAttack(object sender, PlayerController.OnPlayerAttackEventArgs e) {
        if (e.gameObject == gameObject) {
            Destroy(gameObject);
        }
    }

    private void SetMoveDirection() {
        if (transform.position.x > 0) {
            isMoveDirectionLeft = true;
        } else {
            isMoveDirectionLeft = false;
        }
        projectileSpeedX = 0f;
    }

    protected void HandleProjectileHorizontalMovement() {

        if (projectileSpeedX == 0f) {
            projectileSpeedX = Random.Range(projectileSO.minSpeed, projectileSO.maxSpeed);
            projectileSpeedX = isMoveDirectionLeft ? -projectileSpeedX : projectileSpeedX;
        }
        transform.position += new Vector3(projectileSpeedX * Time.deltaTime, 0, 0);

    }

    protected void HandleProjectileVerticalMovement() {
        float projectileSpeedY = GameManager.Instance.GetClimbSpeed();
        transform.position += new Vector3(0, -projectileSpeedY * Time.deltaTime, 0);
    }

    private void DestroyIfOutOfScreen() {
        float outOfBounds = isMoveDirectionLeft ? -2.5f : 2.5f;

        if ((transform.position.x <= outOfBounds && isMoveDirectionLeft) || (transform.position.x >= outOfBounds && !isMoveDirectionLeft)) {
            Destroy(gameObject);
        }
    }
}
