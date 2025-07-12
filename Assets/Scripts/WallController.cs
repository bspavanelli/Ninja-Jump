using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private float speed;

    private float distanceToReset = -9.22f;
    private void Update() {
        if (transform.position.y <= distanceToReset) {
            transform.position = Vector3.zero;
        }
        transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0);
    }
}
