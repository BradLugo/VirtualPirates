using UnityEngine;
using System.Collections;
using System;

public class MonsterDefenseManager : MonoBehaviour, IDefenseManager {

    IHealthManager healthManager;

    private int _defenseValue;
    public int defenseValue
    {
        get
        {
            return _defenseValue;
        }

        set
        {
            _defenseValue = value;
        }
    }

    public void Defend(int attackingValue)
    {
        int currentHealth = healthManager.health;

        healthManager.health = currentHealth - (attackingValue - defenseValue);
    }

    // Use this for initialization
    void Start () {
        healthManager = GetComponent<IHealthManager>();
        defenseValue = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
