using UnityEngine;
using System.Collections;

public class ExplodeSpell : MonoBehaviour {

	public GameObject explosion;

	void OnCollisionEnter(Collision col){

        var target = col.gameObject.GetComponent<IDefenseManager>();
        if (target != null)
        {
            target.Defend(10);
        }
		GameObject expl = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
		Destroy(gameObject);
		Destroy(expl, 3);
	}
}
