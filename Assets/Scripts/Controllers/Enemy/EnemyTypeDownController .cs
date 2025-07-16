using UnityEngine;

public class EnemyTypeDownController : EnemyBaseController {
    private float currentSpeed = 0f;

    protected override void HandleEnemyMovement() {
        if (currentSpeed == 0f) {
            currentSpeed = Random.Range(enemySO.minSpeed, enemySO.maxSpeed);
        }

        float enemySpeed = GameManager.Instance.GetClimbSpeed() + currentSpeed;
        transform.position += new Vector3(0, -enemySpeed * Time.deltaTime, 0);
    }

    protected override void HandleEnemyAttack() {
    }
}
