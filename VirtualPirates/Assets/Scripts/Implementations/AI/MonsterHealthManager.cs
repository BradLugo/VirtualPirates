using UnityEngine;
using System.Collections;
using System;

public class MonsterHealthManager : MonoBehaviour, IHealthManager {

    private int _health;
    public int health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }

    // Use this for initialization
    void Start () {
        _health = 30;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
