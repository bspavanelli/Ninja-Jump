using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private const int WALL_LAYER = 6;
    private const int ENEMY_LAYER = 7;

    public event EventHandler onPlayerDeath;
    public event EventHandler onPlayerAttack;

    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;

    // Posição do player quando está na parede, X varia entre esse valor positivo ou negativo
    private float defaultPositionXLeftWall = -1.73f;
    private float defaultPositionXRightWall = 1.73f;
    private float defaultPositionY = -1.64f;

    private bool isOnLeftWall;
    private bool isJumping;

    void Start() {
        InputManager.Instance.OnJumpAction += InputManager_OnJumpAction;

        rb = GetComponent<Rigidbody2D>();

        isOnLeftWall = true;
        isJumping = false;
    }

    private void OnDestroy() {
        InputManager.Instance.OnJumpAction -= InputManager_OnJumpAction;
    }

    private void InputManager_OnJumpAction(object sender, System.EventArgs e) {
        JumpToOtherWall();
    }

    void JumpToOtherWall() {
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == WALL_LAYER && isJumping) {
            isJumping = false;

            float positionX = isOnLeftWall ? defaultPositionXLeftWall : defaultPositionXRightWall;
            Vector2 onWallPosition = new Vector2(positionX, defaultPositionY);
            transform.position = onWallPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == ENEMY_LAYER) {
            // Se está pulando, mata o inimigo, se está correndo na parede, toma dano.
            if (isJumping) {
                Debug.Log("Atacou inimigo!");
                onPlayerAttack?.Invoke(this, EventArgs.Empty);
            } else {
                Debug.Log("Morreu!");
                GameManager.Instance.GameOver();
            }
        }
    }
}

