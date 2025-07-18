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

    public float positionX;
    public EnemyType type;

    [Header("For enemy with movement Horizontal or Vertical")]
    public float minSpeed;
    public float maxSpeed;
    
    [Header("For SIDEWAYS type of enemy")]
    public GameObject platformPrefab;

    [Header("For STATIC_SHOOT type of enemy")]
    public float minShootPositionY;
    public float maxShootPositionY;
}
