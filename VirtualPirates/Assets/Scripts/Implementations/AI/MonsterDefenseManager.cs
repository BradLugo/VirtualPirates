using UnityEngine;
using System.Collections;
using System;

public class MonsterDefenseManager : MonoBehaviour, IDefenseManager
{

    IHealthManager healthManager;
    MonsterBehaviour behaviour;
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
        if (!behaviour.dead)
        {
            int currentHealth = healthManager.health;

            GetComponent<Animation>().Play("Damage");
            GetComponent<Animation>()["Damage"].speed = 1;
            GetComponent<Animation>()["Damage"].wrapMode = WrapMode.Once;
            healthManager.health = currentHealth - (attackingValue - defenseValue);

            StartCoroutine(wait(GetComponent<Animation>()["Damage"].length));
        }
    }

    public float getSize()
    {
        return 10f;
    }

    // Use this for initialization
    void Start()
    {
        healthManager = GetComponent<IHealthManager>();
        behaviour = GetComponent<MonsterBehaviour>();
        defenseValue = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator wait(float time)
    {
        behaviour.canMove = false;
        yield return new WaitForSeconds(time);
        behaviour.canMove = true;
    }
}
