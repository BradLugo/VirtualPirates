using UnityEngine;
using System.Collections;

public class ObjectiveBehaviour : MonoBehaviour {

    private IHealthManager healthManager;
    private IDefenseManager defenseManager;
    // TODO Worry about destruction manager later
    //private IDestructionManager destructionManager;



	// Use this for initialization
	void Start () {
        healthManager = GetComponent<IHealthManager>();
        defenseManager = GetComponent<IDefenseManager>();
        //destructionManager = GetComponent<IDestructionManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (healthManager.health <= 0)
        {
            Destroy(gameObject);
            //destructionManager.Destroy();
        }
	}
}
