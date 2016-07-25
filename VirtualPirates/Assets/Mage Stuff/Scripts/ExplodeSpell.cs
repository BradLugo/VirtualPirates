using UnityEngine;
using System.Collections;

public class ExplodeSpell : MonoBehaviour {

	public GameObject explosion;
    public AudioClip explodeSound;

	void OnCollisionEnter(Collision col){

        var target = col.gameObject.GetComponent<IDefenseManager>();
        if (target != null)
        {
            target.Defend(10);
        }
       
        GameObject expl = Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
        expl.AddComponent<AudioSource>();

        var audioSource = expl.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.Stop();
        audioSource.clip = explodeSound;
        audioSource.Play();


        Destroy(gameObject);
		Destroy(expl, 3);
      
    }
}
