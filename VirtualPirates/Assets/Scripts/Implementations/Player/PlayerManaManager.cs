using UnityEngine;
using System.Collections;
using System;

public class PlayerManaManager : MonoBehaviour, IResourceManager {

    UnityEngine.UI.Slider manaSlider;

    private int _manaPool;
    public int resourcePool
    {
        get
        {
            return _manaPool;
        }

        set
        {
            manaSlider.value = value;
            _manaPool = value;
        }
    }

    // Use this for initialization
    void Start () {
        manaSlider = GameObject.Find("ManaSlider").GetComponent<UnityEngine.UI.Slider>();
        _manaPool = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
