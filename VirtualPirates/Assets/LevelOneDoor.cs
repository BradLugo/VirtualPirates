using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelOneDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        SceneManager.LoadScene("Training Chamber");
    }
}
