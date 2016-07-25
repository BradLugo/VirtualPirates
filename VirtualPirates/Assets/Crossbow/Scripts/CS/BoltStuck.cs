using UnityEngine;
using System.Collections;

public class BoltStuck : MonoBehaviour {

	private RaycastHit hit;
	public AudioClip hitSound; //sound to play when arrow hits a surface

	void Update() {
		//Only check for obstacles to stick to if the arrow is moving
		if(GetComponent<Rigidbody>().velocity.magnitude > 0.5) {
			CheckForObstacles();
		}
		else {
			//if the arrow's velocity is very little, disable this script
			enabled = false;
		}
	}

	void CheckForObstacles() {
		if(Physics.Raycast(transform.position, transform.forward, out hit, 1.0f)) {
			GetComponent<AudioSource>().Stop();
			GetComponent<AudioSource>().clip = hitSound;
			GetComponent<AudioSource>().Play();
			
			//since we need to disable the boxCollider and freeze the rigidbody for the arrow to stick,
			//we must add our own force to any moveable objects (objects with rigidbodies) that the arrow hits
			if(hit.transform.GetComponent<Rigidbody>()) {
				//add force to object hit
				hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<Rigidbody>().velocity.magnitude * 10);
			}
			
			//disable arrow's collider | if you leave the arrow's collider on, while inside another object, it may have strange effects
			GetComponent<Collider>().enabled = false;
			//if you require the boxCollider to remain active, say for picking up the arrow, you can instead set the collider to trigger:
			//GetComponent(BoxCollider).isTrigger = true;
			
			//freeze rigidbody
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			//position arrow
			transform.position = hit.point;
			//parent arrow to object | if the object the arrow hits moves, this will make the arrow move with it
			transform.parent = hit.transform;
			
			//once the arrow is stuck, disable this script
			enabled = false;
		}
		else {
			//make arrows top-heavy | on arrows with little velocity (when the bow is not drawn back very far) the arrows will rotated toward the ground
			Quaternion newRot = transform.rotation;
			newRot.SetLookRotation(GetComponent<Rigidbody>().velocity);
			transform.rotation = newRot;
		}
	}
	void OnCollisionEnter(Collision collision) {
		if(GetComponent<Rigidbody>().velocity.magnitude > 5) {
			if(collision.transform.tag != "Player") {
				GetComponent<BoxCollider>().enabled = false;
				//freeze rigidbody
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
				//position arrow
				foreach(ContactPoint contact in collision.contacts) {
					transform.position = contact.point;
				}
				//parent arrow to object | if the object the arrow hits moves, this will make the arrow move with it
				transform.parent = collision.transform;
				
				enabled = false;
			}
		}
	}
}
