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
		StartCoroutine("spawnWaves");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void spawnWave(WaveConfig wave) {
		StartCoroutine("spawnWaveEnemies", wave);
	}

	float getRandomWaitFactor (float randomFactor) {
		return 1 + ayn.Range(0, randomFactor);
	}

	IEnumerator spawnWaveEnemies(WaveConfig wave) {
		Debug.Log(wave.getWaypoints()[0].position);
		for (int enemy_count = 0; enemy_count < wave.getNumberOfEnemies(); enemy_count++) {
			var enemy = GameObject.Instantiate(
				wave.getEnemyPrefab(),
				wave.getWaypoints()[0].position,
				Quaternion.identity
			);
			enemy.GetComponent<Enemy>().setMoveSpeed(wave.getMoveSpeed());
			enemy.GetComponent<Enemy>().setWaypoints(wave.getWaypoints());
			yield return new WaitForSeconds(
				wave.getTimeBetweenSpawns() * getRandomWaitFactor(wave.getSpawnRandomFactor()));
		}

	}

	IEnumerator spawnWaves() {
		while (true) {
			foreach (var wave in waves) {;
				spawnWave(wave);
				yield return new WaitForSeconds(timeBetweenWaves * getRandomWaitFactor(waveRandomFactor));
			}
			yield return new WaitForSeconds(timeBetweenWaves * getRandomWaitFactor(waveRandomFactor));		
		}
	}

}
