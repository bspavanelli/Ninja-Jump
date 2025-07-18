using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public event EventHandler<OnPlayerAttackEventArgs> OnPlayerAttack;
    public class OnPlayerAttackEventArgs : EventArgs {
        public GameObject gameObject;
    }

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private GameObject shield;

    private Rigidbody2D rb;

    // Posição do player quando está na parede, X varia entre esse valor positivo ou negativo
    private readonly float defaultPositionX = 1.73f;
    private readonly float defaultPositionY = -1.64f;

    private bool isOnLeftWall;
    private bool isJumping;

    private void Start() {
        InputManager.Instance.OnJumpAction += InputManager_OnJumpAction;

        rb = GetComponent<Rigidbody2D>();

        isOnLeftWall = true;
        isJumping = false;
    }

    private void OnDestroy() {
        InputManager.Instance.OnJumpAction -= InputManager_OnJumpAction;
    }

    private void InputManager_OnJumpAction(object sender, System.EventArgs e) {
        if (GameManager.Instance.GetGameState() == GameManager.State.GamePlaying) {
            JumpToOtherWall();
        }
    }

    private void JumpToOtherWall() {
        if (!isJumping) {
            isJumping = true;

            // Inverte o lado
            isOnLeftWall = !isOnLeftWall;

            // Define a direção horizontal
            float directionMultiplier = isOnLeftWall ? -1f : 1f;

            Vector2 jumpVelocity = new Vector2(directionMultiplier, 0) * jumpForce;
            rb.linearVelocity = jumpVelocity;
        }
    }

    public bool IsJumping() {
        return isJumping;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == GameManager.WALL_LAYER && isJumping) {
            isJumping = false;

            float positionX = isOnLeftWall ? -defaultPositionX : defaultPositionX;
            Vector2 onWallPosition = new Vector2(positionX, defaultPositionY);
            transform.position = onWallPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == GameManager.ENEMY_LAYER || collision.gameObject.layer == GameManager.PROJECTILE_LAYER) {
            // Se está pulando, mata o inimigo, se está correndo na parede, toma dano.
            if (isJumping) {
                OnPlayerAttack?.Invoke(this, new OnPlayerAttackEventArgs {
                    gameObject = collision.gameObject
                });
            } else {
                // Se o shield está ativo, desativa o escudo e ataca o inimigo para destrui-lo
                if (shield.activeInHierarchy) {
                    shield.SetActive(false);
                    OnPlayerAttack?.Invoke(this, new OnPlayerAttackEventArgs {
                        gameObject = collision.gameObject
                    });

                    ShieldSpawnManager.Instance.SetCanSpawnShieldTrue();
                } else {
                    GameManager.Instance.GameOver();
                }
            }
        }

        if (collision.gameObject.layer == GameManager.BUFF_LAYER) {
            if (collision.TryGetComponent<ShieldBuffObjectController>(out ShieldBuffObjectController shieldBuff)) {
                shield.SetActive(true);
                Destroy(shieldBuff.gameObject);
            }
        }
    }
}

