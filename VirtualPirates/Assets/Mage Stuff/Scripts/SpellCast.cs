using UnityEngine;
using System.Collections;

public class SpellCast : MonoBehaviour {

	public Transform spawn;
	public GameObject spell;
	public int spellspeed;

	void Update(){

		if (Input.GetButtonDown ("Fire1")) {

			GameObject magic = Instantiate (spell, spawn.transform.position, spawn.rotation) as GameObject;
			magic.GetComponent<Rigidbody>().AddForce (spawn.forward * spellspeed);
		}
	}
}
