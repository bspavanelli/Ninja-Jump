using UnityEngine;
using static EnemySO;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "Projectile/ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    public string objectName;
    public GameObject prefab;
    public float minSpeed;
    public float maxSpeed;
}
