﻿using UnityEngine;
using System.Collections;

public class CrossbowShoot : MonoBehaviour {

	public Transform arrowSpawn; //the bolt's spawn point
	public Transform projectile; //the bolt to be instantiated
	public AudioClip drawSound; //the sound to be played on reload (when the string and bolt are drawn back)
	public AudioClip shootSound; //the audio to be played on shoot
	public float power = 2000; //how fast the bolt will be shoot
	public float destroyTime; //destroy the instantiated arrow, after this many seconds | if destroyArrows is unchecked, this time will have no effect
	public bool destroyBolts = false; //destroy arrows shortly after they've been shot, or not
	bool actionInProgress = false;
	bool canShoot = false;

	// Use this for initialization
	void Start () {
		//disable cursor on start
		Cursor.visible = false;
		
		//set crossbow's animation to an idle state
		GetComponent<Animation>().Play("Shoot");
		GetComponent<Animation>()["Shoot"].time = GetComponent<Animation>()["Shoot"].length;
		GetComponent<Animation>()["Shoot"].wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			if(actionInProgress == false) {
				//play drawSound
				GetComponent<AudioSource>().Stop();
				GetComponent<AudioSource>().clip = drawSound;
				GetComponent<AudioSource>().Play();
				
				//Play Draw animation On Mouse Down
				GetComponent<Animation>().Play("Reload");
				GetComponent<Animation>()["Reload"].speed = 1;
				GetComponent<Animation>()["Reload"].wrapMode = WrapMode.Once;
				
				//Enable arrowSpawn MeshRenderer
				arrowSpawn.transform.GetComponent<Renderer>().enabled = true;
				
				actionInProgress = true;

				//Wait for Crossbow to load before we can shoot | Peramiters : float
				SendMessage("WaitForAnimation", GetComponent<Animation>()["Reload"].length - 0.2);
			}
		}
		if(Input.GetMouseButtonUp(0)) {
			if(canShoot == true) {
				GetComponent<AudioSource>().Stop();
				GetComponent<AudioSource>().clip = shootSound;
				GetComponent<AudioSource>().Play();
				
				//Play Shoot animation On Mouse Up
				GetComponent<Animation>().Play("Shoot");
				GetComponent<Animation>()["Shoot"].speed = 4;
				GetComponent<Animation>()["Shoot"].wrapMode = WrapMode.Once;
				
				//Disable arrow Spawn MeshRenderer
				arrowSpawn.transform.GetComponent<Renderer>().enabled = false;
				
				//Instantiated projectile (arrow)
				Transform arrow = Instantiate(projectile, arrowSpawn.transform.position, transform.rotation) as Transform;
				
				//Add force to projectile, based off the power var
				arrow.transform.GetComponent<Rigidbody>().AddForce(transform.forward * power);
				
				if(destroyBolts == true) {
					//Destroy instantiated arrow, after given time
					Destroy(arrow.gameObject, destroyTime); //< you can change the amount of time until the arrow is destroyed by chaning destroyTime on the script, in the editor
				}
				
				actionInProgress = false;
				canShoot = false;
			}
		}
	}
	IEnumerator WaitForAnimation(float animLength) {
		//animLength is the time (as float) that we assigned when we sent the message
		yield return new WaitForSeconds(animLength);
		//when time is up, crossbow is loaded and we can shoot
		canShoot = true;
	}
}
