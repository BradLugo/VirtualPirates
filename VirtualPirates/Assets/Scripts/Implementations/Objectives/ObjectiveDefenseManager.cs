using UnityEngine;
using System.Collections;
using System;

public class ObjectiveDefenseManager : MonoBehaviour, IDefenseManager {

    private IHealthManager healthManager;

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
        healthManager.health -= (attackingValue - defenseValue);
    }

    public float getSize()
    {
        return 7f;
    }

    // Use this for initialization
    void Start () {
        defenseValue = 0;
        healthManager = GetComponent<IHealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
