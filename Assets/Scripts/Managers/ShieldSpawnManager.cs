using UnityEngine;

public class ShieldSpawnManager : MonoBehaviour {
    public static ShieldSpawnManager Instance { get; private set; }

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject shieldBuffPrefab;

    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    private bool canSpawnShield;
    private float waitToSpawnTimeMax;
    private float currentTimeToSpawnCounter;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one ShieldSpawnManager Instance");
        }
        Instance = this;
    }

    private void Start() {
        SetCanSpawnShieldTrue();
    }

    private void Update() {
        if (canSpawnShield && GameManager.Instance.GetGameState() == GameManager.State.GamePlaying) {
            currentTimeToSpawnCounter += Time.deltaTime;
            
            if (currentTimeToSpawnCounter > waitToSpawnTimeMax) {
                Instantiate(shieldBuffPrefab);
                canSpawnShield = false;
            }
        }
    }

    public void SetCanSpawnShieldTrue() {
        waitToSpawnTimeMax = Random.Range(minSpawnTime, maxSpawnTime);
        currentTimeToSpawnCounter = 0;
        canSpawnShield = true;
    }
}
