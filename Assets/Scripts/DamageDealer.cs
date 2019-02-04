using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    [SerializeField] int damageAmount;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    private void OnCollisionEnter2D(Collision2D other) {
        try {
            var enemy = other.gameObject;
            var enemy_health = enemy.GetComponent<Health>().HP();
            enemy_health -= damageAmount;
            if (enemy_health <= 0) { Destroy(other.gameObject); }
            else { enemy.GetComponent<Health>().HP(enemy_health); }
        }
        catch {
            Debug.Log(other.gameObject + " has no Health script attached.");    
        }
        Destroy(gameObject);
    }

}