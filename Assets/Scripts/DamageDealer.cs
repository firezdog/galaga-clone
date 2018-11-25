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
            Health otherHealth = other.gameObject.GetComponent<Health>();
            int otherHP = otherHealth.HP();
            otherHealth.HP(otherHP - damageAmount);
        }
        catch {
            Debug.Log(other.gameObject + " has no Health script attached.");    
        }
        Destroy(gameObject);
    }

}