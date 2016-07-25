using UnityEngine;
using System.Collections;
using System;

public class KatanaScript : IWieldable {

    protected Rigidbody rigidbodyInstance;
    protected MeshCollider meshCollider;
    private bool currentlyInteracting = false;

    // velocity_obj = (hand_pos - obj_pos) * velocityFactor / rigidbody.mass
    private float velocityFactor = 9000f;
    private Vector3 posDelta; // posDelta = (hand_pos - obj_pos)

    private float rotationFactor = 100;
    private Quaternion rotationDelta;
    private float angle;
    private Vector3 axis;

    // The controller this object is picked up by
    private ViveRightController attachedController;
    // The point at which the object will be held
    private Transform interactionPoint;

    private int _attackValue;
    public int attackValue
    {
        get
        {
            return _attackValue;
        }

        set
        {
            _attackValue = value;
        }
    }

    public override void EndInteraction(ViveRightController wand)
    {
        if (wand == attachedController || attachedController == null)
        {
            attachedController = null;
            currentlyInteracting = false;
            meshCollider.isTrigger = false;
        }
    }

    public override bool IsInteracting()
    {
        return currentlyInteracting;
    }

    public override void BeginInteraction(ViveRightController wand)
    {
        attachedController = wand;
        meshCollider.isTrigger = true;
        interactionPoint.position = this.transform.position;
        this.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        interactionPoint.rotation = this.transform.rotation;

        //Vector3 rot = interactionPoint.transform.rotation.eulerAngles;

        Vector3 rot = new Vector3(270f, 0, 0);

        interactionPoint.transform.rotation = Quaternion.Euler(rot);


        interactionPoint.SetParent(transform, true);

        currentlyInteracting = true;

     
    }

    void Start()
    {
        rigidbodyInstance = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
        interactionPoint = new GameObject().transform;
        velocityFactor /= rigidbodyInstance.mass;
        rotationFactor /= rigidbodyInstance.mass;
        _attackValue = 20;
    }

    // Update is called once per frame
    void Update () {
        if (attachedController && currentlyInteracting)
        {
            
            posDelta = attachedController.transform.position - interactionPoint.position;

            float distance;
            distance = (attachedController.transform.position - transform.position).sqrMagnitude;

            if (distance < 0.05)
            {
                meshCollider.isTrigger = false;
                this.rigidbodyInstance.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;
            }
            else
            {
                meshCollider.isTrigger = true;
                this.transform.rotation = attachedController.transform.rotation * Quaternion.Euler(90f, 0f, 0f);
                this.transform.position = attachedController.transform.position;
                posDelta = new Vector3(0, 0, 0);
                rotationDelta = new Quaternion();
                this.rigidbodyInstance.velocity = new Vector3(0, 0, 0);
            }


            rotationDelta = attachedController.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
            {
                angle -= 360;
            }

            if (angle != float.NaN)
                this.rigidbodyInstance.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        IDefenseManager targetDefense = collider.gameObject.GetComponentInParent<IDefenseManager>();

        if (targetDefense != null)
        {
            Debug.Log("HIT DAT");

            targetDefense.Defend(_attackValue);
        }
    }
}
