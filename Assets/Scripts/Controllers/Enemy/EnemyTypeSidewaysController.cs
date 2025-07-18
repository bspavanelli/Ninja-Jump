using UnityEngine;

public class EnemyTypeSidewaysController : EnemyBaseController {
    private float currentSpeedX = 0f;
    protected override void HandleEnemyMovement() {
        float playerY = GameManager.Instance.GetPlayerController().transform.position.y;
        float climbSpeed = GameManager.Instance.GetClimbSpeed();

        // Cálculo do tempo até o inimigo alcançar o player em Y
        float timeToReachPlayerY = (transform.position.y - playerY) / climbSpeed;

        // Verifica se está indo na direção da parede onde o player está
        bool playerOnLeftWall = GameManager.Instance.GetPlayerController().transform.position.x < 0;
        bool isHeadingTowardsPlayerWall = (playerOnLeftWall == isMoveDirectionLeft);

        if (currentSpeedX == 0f) {
            currentSpeedX = enemySO.minSpeed;
        }

        if (isHeadingTowardsPlayerWall && timeToReachPlayerY > 0f && transform.position.y > playerY) {
            // Calcula a distância até a parede onde o player está
            float targetX = playerOnLeftWall ? -enemySO.positionX : enemySO.positionX;
            float distanceToTargetX = Mathf.Abs(targetX - transform.position.x);

            // Velocidade necessária para chegar na parede em X no mesmo tempo
            currentSpeedX = distanceToTargetX / timeToReachPlayerY;

            // Verifica se a velocidade determinada é menor ou maior a minima/máxima e ajusta para os valores mínimos/máximos definidos.
            if (currentSpeedX < enemySO.minSpeed) {
                currentSpeedX = enemySO.minSpeed;
            } else if (currentSpeedX > enemySO.maxSpeed) {
                currentSpeedX = enemySO.maxSpeed;
            }
        }

        // Inverte direção ao bater nas bordas
        if (isMoveDirectionLeft && transform.position.x <= -enemySO.positionX) {
            isMoveDirectionLeft = false;
        } else if (!isMoveDirectionLeft && transform.position.x >= enemySO.positionX) {
            isMoveDirectionLeft = true;
        }


        // Define a direção de movimento
        float moveXDirection = (isMoveDirectionLeft ? -1f : 1f) * currentSpeedX * Time.deltaTime;

        // Aplica movimento (X e Y)
        float enemySpeedY = climbSpeed;
        transform.position += new Vector3(moveXDirection, -enemySpeedY * Time.deltaTime, 0);
    }

    protected override void HandleEnemyAttack() {
        // Sem ataque por enquanto
    }
}
