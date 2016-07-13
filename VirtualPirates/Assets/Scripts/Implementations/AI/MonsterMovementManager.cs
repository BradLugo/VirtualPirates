using UnityEngine;
using System.Collections;
using System;

public class MonsterMovementManager : MonoBehaviour, IMovementManager {

    private float rotateSpeed;

    Animator animator;
    Animation animation;

    private int _jumpHeight;
    public int jumpHeight
    {
        get
        {
            return _jumpHeight;
        }

        set
        {
            _jumpHeight = value; 
        }
    }

    private int _moveSpeed;
    public int moveSpeed
    {
        get
        {
            return _moveSpeed;
        }

        set
        {
            _moveSpeed = value;
        }
    }

    public void Move(Vector3 direction)
    {

        Vector3 targetRotate;

        targetRotate = Vector3.RotateTowards(transform.forward, direction - transform.position, Time.deltaTime * 2f, 1.0f);

        transform.rotation = Quaternion.LookRotation(targetRotate);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, direction, step);
        
    }

    public void Update()
    {
        //foreach (AnimationState state in animation)
        //{
        //    state.speed = 0.5f;
        //}
        
    }

    // Use this for initialization
    void Start () {
        _moveSpeed = 1;
        _jumpHeight = 1;
        //animator = GetComponent<Animator>();
        //animation = GetComponent<Animation>();
        //Debug.Log("Start animation");

        //animator.speed = 1;
        //animator.StartPlayback();
	}

}
