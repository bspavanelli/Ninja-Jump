using NUnit.Framework;
using UnityEngine;

public class EnemyTypeStaticShooterController : EnemyBaseController {
    [SerializeField] private ProjectileSO projectileSO;

    private float currentSpeed;
    private bool hasMadeAttack = false;

    protected override void HandleEnemyMovement() {
        if (currentSpeed == 0f) {
            currentSpeed = Random.Range(enemySO.minSpeed, enemySO.maxSpeed);
        }

        float enemySpeed = GameManager.Instance.GetClimbSpeed() + currentSpeed;
        transform.position += new Vector3(0, -enemySpeed * Time.deltaTime, 0);
    }

    protected override void HandleEnemyAttack() {
        if (!hasMadeAttack) {
            float attackPosition = Random.Range(enemySO.minShootPositionY, enemySO.maxShootPositionY);
            
            // Aplicar lógica para ver quando o projetil será atirado, baseado na distância entre o inimigo e o player
            if (!hasMadeAttack && transform.position.y <= attackPosition) {

                // Instancia um projetil
                Transform projectile = Instantiate(projectileSO.prefab).transform;
                projectile.position = transform.position;
                hasMadeAttack = true;
            }
        }
    }
}
