using UnityEngine;

public class EnemyTypeDownController : EnemyBaseController {
    protected override void HandleEnemyMovement() {
        float enemySpeed = GameManager.Instance.GetClimbSpeed() + enemySO.speed;
        transform.position += new Vector3(0, -enemySpeed * Time.deltaTime, 0);
    }

    protected override void HandleEnemyAttack() {
    }
}
