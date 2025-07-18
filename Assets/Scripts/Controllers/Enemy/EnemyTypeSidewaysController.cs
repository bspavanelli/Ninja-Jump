using UnityEngine;

public class EnemyTypeSidewaysController : EnemyBaseController {
    private float currentSpeedX = 0f;
    protected override void HandleEnemyMovement() {
        float playerY = GameManager.Instance.GetPlayerController().transform.position.y;
        float climbSpeed = GameManager.Instance.GetClimbSpeed();

        // C�lculo do tempo at� o inimigo alcan�ar o player em Y
        float timeToReachPlayerY = (transform.position.y - playerY) / climbSpeed;

        // Verifica se est� indo na dire��o da parede onde o player est�
        bool playerOnLeftWall = GameManager.Instance.GetPlayerController().transform.position.x < 0;
        bool isHeadingTowardsPlayerWall = (playerOnLeftWall == isMoveDirectionLeft);

        if (currentSpeedX == 0f) {
            currentSpeedX = enemySO.minSpeed;
        }

        if (isHeadingTowardsPlayerWall && timeToReachPlayerY > 0f && transform.position.y > playerY) {
            // Calcula a dist�ncia at� a parede onde o player est�
            float targetX = playerOnLeftWall ? -enemySO.positionX : enemySO.positionX;
            float distanceToTargetX = Mathf.Abs(targetX - transform.position.x);

            // Velocidade necess�ria para chegar na parede em X no mesmo tempo
            currentSpeedX = distanceToTargetX / timeToReachPlayerY;

            // Verifica se a velocidade determinada � menor ou maior a minima/m�xima e ajusta para os valores m�nimos/m�ximos definidos.
            if (currentSpeedX < enemySO.minSpeed) {
                currentSpeedX = enemySO.minSpeed;
            } else if (currentSpeedX > enemySO.maxSpeed) {
                currentSpeedX = enemySO.maxSpeed;
            }
        }

        // Inverte dire��o ao bater nas bordas
        if (isMoveDirectionLeft && transform.position.x <= -enemySO.positionX) {
            isMoveDirectionLeft = false;
        } else if (!isMoveDirectionLeft && transform.position.x >= enemySO.positionX) {
            isMoveDirectionLeft = true;
        }


        // Define a dire��o de movimento
        float moveXDirection = (isMoveDirectionLeft ? -1f : 1f) * currentSpeedX * Time.deltaTime;

        // Aplica movimento (X e Y)
        float enemySpeedY = climbSpeed;
        transform.position += new Vector3(moveXDirection, -enemySpeedY * Time.deltaTime, 0);
    }

    protected override void HandleEnemyAttack() {
        // Sem ataque por enquanto
    }
}
