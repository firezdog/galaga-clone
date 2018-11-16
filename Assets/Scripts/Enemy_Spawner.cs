using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ayn = UnityEngine.Random;

public class Enemy_Spawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waves;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] float waveRandomFactor = 0.3f;

    // Use this for initialization
    void Start () {
        StartCoroutine ("spawnWaves");
    }

    // Update is called once per frame
    void Update () { }

    IEnumerator spawnWaves () {
        while (true) {
            float waitTime;
            foreach (var wave in waves) {;
                StartCoroutine ("spawnWaveEnemies", wave);
                waitTime = getRandomWaitFactor (waveRandomFactor, timeBetweenWaves);
                yield return new WaitForSeconds (waitTime);
            }
            waitTime = getRandomWaitFactor (waveRandomFactor, timeBetweenWaves);
            yield return new WaitForSeconds (waitTime);
        }
    }

    IEnumerator spawnWaveEnemies (WaveConfig wave) {
        var numberOfEnemies = wave.getNumberOfEnemies ();
        for (int enemy_count = 0; enemy_count < numberOfEnemies; enemy_count++) {
            instantiateEnemy (wave);
            var waitTime = getRandomWaitFactor (wave.getSpawnRandomFactor (), wave.getTimeBetweenSpawns ());
            yield return new WaitForSeconds (waitTime);
        }
    }

    float getRandomWaitFactor (float randomFactor, float waitTime) {
        var randomMultiplier = 1 + ayn.Range (0, randomFactor);
        return waitTime * randomMultiplier;
    }

    private void instantiateEnemy (WaveConfig wave) {
        var enemy = GameObject.Instantiate (
            wave.getEnemyPrefab (),
            wave.getWaypoints () [0].position,
            Quaternion.identity
        );
        enemy.GetComponent<Enemy> ().setMoveSpeed (wave.getMoveSpeed ());
        enemy.GetComponent<Enemy> ().setWaypoints (wave.getWaypoints ());
    }

}