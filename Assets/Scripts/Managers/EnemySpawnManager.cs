using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {
    [SerializeField] private float minEnemySpawnTime;
    [SerializeField] private float maxEnemySpawnTime;
    [SerializeField] private List<EnemySO> enemySOList;

    private void Start() {
        GameManager.Instance.OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void OnDestroy() {
        GameManager.Instance.OnGameStateChange -= GameManager_OnGameStateChange;
    }

    private void GameManager_OnGameStateChange(object sender, System.EventArgs e) {
        if (GameManager.Instance.GetGameState() == GameManager.State.GamePlaying) {
            StartCoroutine(HandleEnemySpawn());
        }
    }

    IEnumerator HandleEnemySpawn() {
        while (GameManager.Instance.GetGameState() == GameManager.State.GamePlaying) {
            SpawnEnemy();

            yield return new WaitForSeconds(Random.Range(minEnemySpawnTime, maxEnemySpawnTime));
        }
    }

    private void SpawnEnemy() {
        int listPosition = Random.Range(0, enemySOList.Count);

        EnemySO enemySO = enemySOList[listPosition];

        if (enemySO.platformPrefab != null) {
            Instantiate(enemySO.platformPrefab);
        }

        Instantiate(enemySO.prefab);
    }
}
