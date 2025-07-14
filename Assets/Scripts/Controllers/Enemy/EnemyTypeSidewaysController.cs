using UnityEngine;

public class EnemyTypeSidewaysController : EnemyBaseController {
    protected override void HandleEnemyMovement() {
        float moveXDirection = 0;

        if (isMoveDirectionLeft) {
            // Verifica se chegou no ponto máximo da esquerda e se sim, muda a direção
            if (transform.position.x <= -enemySO.positionX) {
                // Passa a mover para a direita
                isMoveDirectionLeft = false;
            }
        } else {
            // Verifica se chegou no ponto máximo da direita e se sim, muda a direção
            if (transform.position.x >= enemySO.positionX) {
                // Passa a mover para a esquerda
                isMoveDirectionLeft = true;
            }
        }

        moveXDirection = (isMoveDirectionLeft ? -enemySO.positionX : enemySO.positionX) * Time.deltaTime;

        float enemySpeedY = GameManager.Instance.GetClimbSpeed();

        transform.position += new Vector3(moveXDirection, -enemySpeedY * Time.deltaTime, 0);
    }

    protected override void HandleEnemyAttack() {
    }
}
