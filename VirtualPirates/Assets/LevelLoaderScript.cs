using UnityEngine;
using System.Collections;

public class LevelLoaderScript : MonoBehaviour {

    public GameObject playerWeapon;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        var player = GameObject.FindWithTag("Player");
        var item = player.GetComponentInChildren<ViveRightController>().interactingItem;
        if (item != null)
        {
            this.playerWeapon = item.gameObject;
            DontDestroyOnLoad(playerWeapon);
        }
	}
}
