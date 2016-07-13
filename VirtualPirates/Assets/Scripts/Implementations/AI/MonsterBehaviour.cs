using UnityEngine;
using System.Collections;

public class MonsterBehaviour : MonoBehaviour {

    // Self describing features
    IMovementManager movementManager;
    IAttackManager attackManager;
    IDefenseManager defenseManager;
    IHealthManager healthManager;

    // Related to game behavior
    IDefenseManager targetDefense;
    Vector3 targetPosition;

    // Use this for initialization
	void Start () {
        movementManager = GetComponent<IMovementManager>();
        attackManager = GetComponent<IAttackManager>();
        defenseManager = GetComponent<IDefenseManager>();
        healthManager = GetComponent<IHealthManager>();

        targetDefense = GameObject.FindWithTag("Objective").GetComponent<IDefenseManager>();
        targetPosition = GameObject.FindWithTag("Objective").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if ((transform.position - targetPosition).sqrMagnitude < attackManager.attackRangeSqr)
        {
            attackManager.attack(targetDefense);
        }
        else
        {
            movementManager.Move(targetPosition);
        }

        if (healthManager.health <= 0)
        {
            Destroy(gameObject);
        }
	}
}
