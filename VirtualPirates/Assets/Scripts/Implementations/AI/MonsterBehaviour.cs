using UnityEngine;
using System.Collections;
using System;

public class MonsterBehaviour : MonoBehaviour {

    // Self describing features
    IMovementManager movementManager;
    IAttackManager attackManager;
    IDefenseManager defenseManager;
    IHealthManager healthManager;

    // Related to game behavior
    IDefenseManager targetDefense;
    Vector3 targetPosition;
    GameObject target;

    public bool dead = false;
    public bool canMove = true;

    float nextAttack = 0;

    // Use this for initialization
	void Start () {
        movementManager = GetComponent<IMovementManager>();
        attackManager = GetComponent<IAttackManager>();
        defenseManager = GetComponent<IDefenseManager>();
        healthManager = GetComponent<IHealthManager>();

        targetDefense = GameObject.FindWithTag("Objective").GetComponent<IDefenseManager>();
        targetPosition = GameObject.FindWithTag("Objective").transform.position;
        target = GameObject.FindWithTag("Objective");
        canMove = true;
        dead = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (healthManager.health <= 0)
        {
            if (!dead)
            {
                Destroy(gameObject, 10);
                GetComponent<Animation>().Play("Death");
                GetComponent<Animation>()["Death"].speed = 1;
                GetComponent<Animation>()["Death"].wrapMode = WrapMode.Once;
            }
            dead = true;
        }
        else if (target == null)
        {
            GetComponent<Animation>().Play("Stand");
            GetComponent<Animation>()["Stand"].speed = 1;
            GetComponent<Animation>()["Stand"].wrapMode = WrapMode.Once;
            target = GameObject.FindWithTag("Objective");
        }
        else if ((transform.position - targetPosition).sqrMagnitude - Math.Pow(targetDefense.getSize(), 2) < attackManager.attackRangeSqr)
        {

            GetComponent<Animation>().Play("Attack");
            GetComponent<Animation>()["Attack"].speed = 1;
            GetComponent<Animation>()["Attack"].wrapMode = WrapMode.Once;

            if (Time.time > nextAttack)
            {
                attackManager.attack(targetDefense);
                nextAttack = Time.time + GetComponent<Animation>()["Attack"].length;
            }
        }
        else if (canMove)
        {
            movementManager.Move(targetPosition);
            nextAttack = Time.time + (GetComponent<Animation>()["Attack"].length - 1.7f);
        }

	}

}
