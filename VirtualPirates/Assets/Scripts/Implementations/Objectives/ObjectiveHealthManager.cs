using UnityEngine;
using System.Collections;
using System;

public class ObjectiveHealthManager : MonoBehaviour, IHealthManager {

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
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
