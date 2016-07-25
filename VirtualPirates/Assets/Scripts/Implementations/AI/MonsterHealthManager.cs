using UnityEngine;
using System.Collections;
using System;

public class MonsterHealthManager : MonoBehaviour, IHealthManager {

    public int _health;
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
        _health = 40;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
