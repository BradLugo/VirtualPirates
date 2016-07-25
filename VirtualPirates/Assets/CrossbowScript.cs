using UnityEngine;
using System.Collections;
using System;

public class CrossbowScript : IWieldable {

    protected Rigidbody rigidbody;
    protected Collider collider;
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
    private PlayerManaManager manaPool;
    // The point at which the object will be held
    private Transform interactionPoint;

    // Variables used for shooting the bolts
    bool actionInProgress = false;
    bool canShoot = false;

    public AudioClip drawSound;
    public AudioClip shootSound;
    public GameObject arrowSpawn;
    public GameObject projectile;
    public float power = 20f;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        interactionPoint = new GameObject().transform;
        //arrowSpawn = this.transform;
        velocityFactor /= rigidbody.mass;
        rotationFactor /= rigidbody.mass;
    }
	
	// Update is called once per frame
	void Update () {
        if (attachedController && currentlyInteracting)
        {

            posDelta = attachedController.transform.position - interactionPoint.position;

            float distance;
            distance = (attachedController.transform.position - transform.position).sqrMagnitude;

            if (distance > 0.03)
            {
                this.rigidbody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;
            }
            else
            {
                this.transform.rotation = attachedController.transform.rotation * Quaternion.Euler(0f, 0f, 0f);
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
            //arrowSpawn = attachedController.transform;
            //this.rigidbody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
        }
    }

    public override void BeginInteraction(ViveRightController wand)
    {
        attachedController = wand;
        collider.isTrigger = true;
        interactionPoint.position = this.transform.position;
        interactionPoint.rotation = this.transform.rotation;

        Vector3 rot = interactionPoint.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z + 180);

        interactionPoint.transform.rotation = Quaternion.Euler(rot);

        interactionPoint.SetParent(transform, true);

        wand.GetComponent<SteamVR_TrackedController>().TriggerClicked += TriggerClicked;
        manaPool = wand.GetComponentInParent<PlayerManaManager>();
        currentlyInteracting = true;
    }

    private void TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (actionInProgress == false)
        {
            //play drawSound
            var audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.clip = drawSound;
            audioSource.Play();


            //Play Draw animation On Mouse Down
            GetComponent<Animation>().Play("Reload");
            GetComponent<Animation> ()["Reload"].speed = 1;
            GetComponent<Animation>()["Reload"].wrapMode = WrapMode.Once;

            //Enable arrowSpawn MeshRenderer
            arrowSpawn.GetComponent<MeshRenderer>().enabled = true;

            actionInProgress = true;
            new WaitForSeconds(GetComponent<Animation>()["Reload"].length - 0.2f);
            canShoot = true;
            return;
            //Wait for Crossbow to load before we can shoot | Peramiters : float
        }
        if (canShoot == true)
        {
            GetComponent< AudioSource > ().Stop();
            GetComponent< AudioSource > ().clip = shootSound;
            GetComponent< AudioSource > ().Play();

            //Play Shoot animation On Mouse Up
            GetComponent< Animation > ().Play("Shoot");
            GetComponent< Animation > ()["Shoot"].speed = 4;
            GetComponent< Animation > ()["Shoot"].wrapMode = WrapMode.Once;

            //Disable arrow Spawn MeshRenderer
            arrowSpawn.GetComponent<MeshRenderer>().enabled = false;

            //Instantiated projectile (arrow)
            GameObject arrow = Instantiate(projectile, attachedController.transform.position, attachedController.transform.rotation) as GameObject;
            //Add force to projectile, based off the power var
            arrow.transform.GetComponent<Rigidbody>().AddForce(transform.forward * power);

           //Destroy instantiated arrow, after given time
           Destroy(arrow.gameObject, 15.0f); //< you can change the amount of time until the arrow is destroyed by chaning destroyTime on the script, in the editor

            actionInProgress = false;
            canShoot = false;
        }
    }

    public override void EndInteraction(ViveRightController wand)
    {
        if (wand == attachedController)
        {
            attachedController = null;
            currentlyInteracting = false;
            collider.isTrigger = false;
            wand.GetComponent<SteamVR_TrackedController>().TriggerClicked -= TriggerClicked;
        }
    }

    public override bool IsInteracting()
    {
        return currentlyInteracting;
    }

}
