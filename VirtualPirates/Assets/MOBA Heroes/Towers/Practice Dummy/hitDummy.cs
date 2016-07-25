using UnityEngine;
using System.Collections;

public class hitDummy : MonoBehaviour {

	private Animator DummyAnimator;
	public GameObject hay;

	void Start () {
		DummyAnimator = GetComponent<Animator>();
	}
	
	void OnMouseDown()
	{
		DummyAnimator.SetTrigger("hit");
		Instantiate(hay, transform.position + Vector3.up, Quaternion.identity);
	}

    public void TakeHit()
    {
        DummyAnimator.SetTrigger("hit");
        Instantiate(hay, transform.position + Vector3.up, Quaternion.identity);
    }

    private void onTriggerEnter(Collider collider)
    {
        DummyAnimator.SetTrigger("hit");
        Instantiate(hay, transform.position + Vector3.up, Quaternion.identity);
    }	
}
