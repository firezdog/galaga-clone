﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	
	[SerializeField] private int hp;
	public int HP () { return hp; }
	public void HP (int new_hp) { hp = new_hp; }

}
