using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private float minEnemySpawnTime;
    [SerializeField] private float maxEnemySpawnTime;
    [SerializeField] private List<EnemySO> enemySOList;

    private void Start() {
        GameManager.Instance.onGameStateChange += GameManager_onGameStateChange;
    }

    private void OnDestroy() {
        GameManager.Instance.onGameStateChange -= GameManager_onGameStateChange;
    }

    private void GameManager_onGameStateChange(object sender, System.EventArgs e) {
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
        Debug.Log("Spawnando enemy!");
        EnemySO enemySO = enemySOList[Random.Range(0, enemySOList.Count)];
        
        Instantiate(enemySO.prefab);
    }
}
