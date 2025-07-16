using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Enemy/EnemySO")]
public class EnemySO : ScriptableObject
{
    public enum EnemyType {
        DOWN,
        SIDEWAYS,
        STATIC_SHOOT,
        FLY
    }

    public string objectName;
    public GameObject prefab;
    public GameObject platformPrefab;
    public float positionX;
    public EnemyType type;
    public float minSpeed;
    public float maxSpeed;
}
