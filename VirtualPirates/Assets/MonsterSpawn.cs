using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour {

    // The monster we are going to spawn
    public GameObject monster;

    // The number of monsters we want
    public int count;
    int i;
    public float nextSpawn;
	// Use this for initialization
	void Start () {
        i = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (i < count && nextSpawn < Time.timeSinceLevelLoad)
        {
            Instantiate(monster, this.transform.position, this.transform.rotation);
            nextSpawn = Time.timeSinceLevelLoad + 2;
            i++;
        }
	}
}
