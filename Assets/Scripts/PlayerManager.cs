using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private const int WALL_LAYER = 6;

    public float jumpForce = 10f;

    private bool isJumping;
    private Rigidbody2D rb;
    private bool isOnLeftWall = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        isJumping = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            JumpToOtherWall();
        }
    }

    void JumpToOtherWall() {
        isJumping = true;

        // Inverte o lado
        isOnLeftWall = !isOnLeftWall;

        // Define a direção horizontal
        float directionMultiplier = isOnLeftWall ? -1f : 1f;

        Vector2 jumpVelocity = new Vector2(directionMultiplier, 0) * jumpForce;
        rb.linearVelocity = jumpVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == WALL_LAYER && isJumping) {
            isJumping = false;
        }
    }
}
