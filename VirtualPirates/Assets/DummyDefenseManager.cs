using UnityEngine;
using System.Collections;
using System;

public class DummyDefenseManager : MonoBehaviour, IDefenseManager {
    int _defenseValue;
    public int defenseValue
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public void Defend(int attackingValue)
    {
        GetComponent<hitDummy>().TakeHit();
    }

    public float getSize()
    {
        return 1f;
    }

    // Use this for initialization
    void Start () {
        _defenseValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
