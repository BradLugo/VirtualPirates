using UnityEngine;
using System.Collections;

public class MagicDeath : MonoBehaviour {

	public GameObject explosion;
	public int health;
	public int damage;

	void OnCollisionEnter(Collision col){

		if(col.gameObject.tag == "magic elements"){
			health -= damage;
		}
	}
	

	void Update () {

		if (health <= 0) {

			GameObject expl = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
			Destroy(gameObject);
			Destroy(expl, 3);
		}
	
	}
}
