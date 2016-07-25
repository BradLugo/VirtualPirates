using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowerScript : MonoBehaviour {

    private LinkedList<Collider> targetsInRange = new LinkedList<Collider>();
    private Collider target;

    public Transform spawn;
    public GameObject spell;
    public int spellspeed;

    private float attackSpeed = 1.0F;
    private float nextFire;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (targetsInRange.First != null)
        {
            if (targetsInRange.First.Value != null)
                target = targetsInRange.First.Value;
            else
                targetsInRange.RemoveFirst();
        }
        if (target != null)
        {
            Vector3 targetRotate;

            targetRotate = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, Time.deltaTime * 2f, 1.0f);

            transform.rotation = Quaternion.LookRotation(targetRotate);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            Debug.Log("Attacking");
            Debug.Log(target.name);
            if (Time.time > nextFire)
            {
                nextFire = Time.time + attackSpeed;
                Attack(target);
            }
            
        }
    }

    private void Attack(Collider target)
    {
        GameObject magic = Instantiate(spell, this.transform.position, this.transform.rotation) as GameObject;
        magic.GetComponent<Rigidbody>().AddForce(this.transform.forward * spellspeed);
    }

    private void OnTriggerEnter(Collider collider)
    {
        IDefenseManager target = collider.GetComponent<IDefenseManager>();
        if (target != null && collider.gameObject.tag == "Monster")
        {
            targetsInRange.AddLast(collider);
        }
    }

    // Remove all items no longer colliding with to avoid further processing
    private void OnTriggerExit(Collider collider)
    {
        IWieldable collidedItem = collider.GetComponent<IWieldable>();
        if (target != null && collider.gameObject.tag == "Monster")
        {
            targetsInRange.Remove(collider);
        }
    }
}
