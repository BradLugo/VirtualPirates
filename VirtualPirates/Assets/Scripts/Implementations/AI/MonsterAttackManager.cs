using UnityEngine;
using System.Collections;
using System;

public class MonsterAttackManager : MonoBehaviour, IAttackManager {

    private double _attackRangeSqr;
    public double attackRangeSqr
    {
        get
        {
            return _attackRangeSqr;
        }
        set
        {
            _attackRangeSqr = value;
        }
    }

    private int _attackValue;
    public int attackValue
    {
        get
        {
            return _attackValue;
        }
        set
        {
            _attackValue = value;
        }
    }

    public void attack(IDefenseManager target)
    {
        target.Defend(attackValue);
    }

    // Use this for initialization
    void Start () {
        attackValue = 5;
        attackRangeSqr = Math.Pow(5.5, 2);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
