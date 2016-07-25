using UnityEngine;
using System.Collections;

public class BoltAttack : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        var target = col.gameObject.GetComponent<IDefenseManager>();
        if (target != null)
        {
            target.Defend(15);
        }
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 5)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            foreach (var contactPoint in col.contacts)
            {
                transform.position = contactPoint.point;
            }
            transform.parent = col.transform;
            enabled = false;
        }
    }
}
