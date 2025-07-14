using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : EnemyBaseController {

    protected override void HandleEnemyMovement() {
        float enemySpeed = GameManager.Instance.GetClimbSpeed() + enemySO.speed;
        transform.position += new Vector3(0, -1 * enemySpeed * Time.deltaTime, 0);
    }

    protected override void HandleEnemyAttack() {
    }
}
