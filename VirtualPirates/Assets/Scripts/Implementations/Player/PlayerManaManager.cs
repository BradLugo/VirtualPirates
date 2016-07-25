using UnityEngine;
using System.Collections;
using System;

public class PlayerManaManager : MonoBehaviour, IResourceManager {

    UnityEngine.UI.Slider manaSlider;

    private float nextRegen;
    private float regenRate;

    private int _manaPool;
    public int resourcePool
    {
        get
        {
            return _manaPool;
        }

        set
        {
            //manaSlider.value = value;
            _manaPool = value;
        }
    }

    // Use this for initialization
    void Start () {
        //manaSlider = GameObject.Find("ManaSlider").GetComponent<UnityEngine.UI.Slider>();
        _manaPool = 100;
        regenRate = 2f;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time > nextRegen && _manaPool < 100)
        {
            nextRegen = Time.time + regenRate;
            resourcePool += 10;
        }
        if (resourcePool > 100)
        {
            resourcePool = 100;
        }
	}
}
