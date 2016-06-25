using UnityEngine;
using System.Collections;
using System;

public class KatanaScript : IWieldable {

    protected Rigidbody rigidbody;
    private bool currentlyInteracting = false;

    // velocity_obj = (hand_pos - obj_pos) * velocityFactor / rigidbody.mass
    private float velocityFactor = 20000f;
    private Vector3 posDelta; // posDelta = (hand_pos - obj_pos)

    private float rotationFactor = 10000;
    private Quaternion rotationDelta;
    private float angle;
    private Vector3 axis;

    // The controller this object is picked up by
    private ViveRightController attachedController;
    // The point at which the object will be held
    private Transform interactionPoint;

    public int attackValue
    {
        get
        {
            return attackValue;
        }

        set
        {
            attackValue = value;
        }
    }

    public override void EndInteraction(ViveRightController wand)
    {
        if (wand == attachedController)
        {
            attachedController = null;
            currentlyInteracting = false;
        }
    }

    public override bool IsInteracting()
    {
        return currentlyInteracting;
    }

    public override void BeginInteraction(ViveRightController wand)
    {
        attachedController = wand;

        interactionPoint.position = this.transform.position;
        interactionPoint.rotation = this.transform.rotation;

        Vector3 rot = interactionPoint.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z + 180);

        interactionPoint.transform.rotation = Quaternion.Euler(rot);

        interactionPoint.SetParent(transform, true);

        currentlyInteracting = true;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
        velocityFactor /= rigidbody.mass;
        rotationFactor /= rigidbody.mass;
    }

    // Update is called once per frame
    void Update () {
        if (attachedController && currentlyInteracting)
        {
            Debug.Log("Katana item is interacting");

            posDelta = attachedController.transform.position - interactionPoint.position;

            float distance;
            distance = (attachedController.transform.position - transform.position).sqrMagnitude;

            if (distance > 0.03)
            {
                this.rigidbody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;
            }
            else
            {
                this.transform.rotation = attachedController.transform.rotation * Quaternion.Euler(0f, 180f, 180f); 
                this.transform.position = attachedController.transform.position;
                posDelta = new Vector3(0, 0, 0);
                rotationDelta = new Quaternion();
                this.rigidbody.velocity = new Vector3(0, 0, 0);
            }


            rotationDelta = attachedController.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
            {
                angle -= 360;
            }

            this.rigidbody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
        }
    }

}
