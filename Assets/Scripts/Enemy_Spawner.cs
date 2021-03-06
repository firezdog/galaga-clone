using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ayn = UnityEngine.Random;

public class Enemy_Spawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waves;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] float waveRandomFactor = 0.3f;
    [SerializeField] bool playing = false;

    // Use this for initialization
    IEnumerator Start () {
        startGame();
        while (playing) {
            yield return StartCoroutine ("spawnWaves");
        }
    }

    private void startGame()
    {
        playing = true;
    }

    // Update is called once per frame
    void Update () { }

    IEnumerator spawnWaves () {
        foreach (var wave in waves) {
            yield return StartCoroutine("spawnWaveEnemies", wave);
        }
    }

    IEnumerator spawnWaveEnemies (WaveConfig wave) {
        float waitTime;
        var numberOfEnemies = wave.getNumberOfEnemies ();
        for (int enemy_count = 0; enemy_count < numberOfEnemies; enemy_count++) {
            instantiateEnemy (wave);
            waitTime = getRandomWaitTime (wave.getSpawnRandomFactor (), wave.getTimeBetweenSpawns ());
            yield return new WaitForSeconds (waitTime);
        }
        yield return new WaitForSeconds (getRandomWaitTime (waveRandomFactor, timeBetweenWaves));
    }

    float getRandomWaitTime (float randomFactor, float waitTime) {
        var randomMultiplier = 1 + ayn.Range (0, randomFactor);
        return waitTime * randomMultiplier;
    }

    private void instantiateEnemy (WaveConfig wave) {
        var enemy = GameObject.Instantiate (
            wave.getEnemyPrefab (),
            wave.getWaypoints () [0].position,
            wave.getEnemyPrefab ().transform.rotation
        );
        enemy.GetComponent<Enemy> ().setMoveSpeed (wave.getMoveSpeed ());
        enemy.GetComponent<Enemy> ().setWaypoints (wave.getWaypoints ());
    }

}