using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SteamVR_TrackedController))]
public class ViveRightController : MonoBehaviour
{
    Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    SteamVR_Controller.Device controllerDevice { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    SteamVR_TrackedObject trackedObj;

    private HashSet<IWieldable> objectsHoveringOver = new HashSet<IWieldable>();

    private IWieldable closestItem;
    private IWieldable interactingItem;
    
    void OnEnable()
    {
        SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>(); 
        controller.TriggerClicked += OnClickTrigger;
        controller.TriggerUnclicked += OnUnclickTrigger;
        controller.PadClicked += OnPadClicked;
    }

    void OnDisable()
    {
        SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked -= OnClickTrigger;
        controller.TriggerUnclicked -= OnUnclickTrigger;
        controller.PadClicked -= OnPadClicked;
    }

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void OnPadClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("Right: Pad Clicked! X: " + e.padX + " " + e.padY);
    }

    void OnUnclickTrigger(object sender, ClickedEventArgs e)
    {
        Debug.Log("Right: Unclicked trigger!");
    }

    void OnClickTrigger(object sender, ClickedEventArgs e)
    {
        if (!interactingItem)
        {
            Debug.Log("Right: Clicked trigger!");
            // Find the closest item to the hand in case there are multiple and interact with it
            float minDistance = float.MaxValue;

            float distance;
            foreach (IWieldable item in objectsHoveringOver)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }
            Debug.Log(closestItem.name);
            interactingItem = closestItem;
            closestItem = null;

            if (interactingItem != null)
            {
                if (interactingItem.IsInteracting())
                {
                    interactingItem.EndInteraction(this);
                }

                interactingItem.BeginInteraction(this);
            }
        }
        else
        {
            Debug.Log("Right: End interaction");
            interactingItem.EndInteraction(this);
            interactingItem = null;
        }
        
    }

    // Adds all colliding items to a HashSet for processing which is closest
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Intersected collider");
        IWieldable collidedItem = collider.GetComponent<IWieldable>();
        if (collidedItem)
        {
            objectsHoveringOver.Add(collidedItem);
        }
    }

    // Remove all items no longer colliding with to avoid further processing
    private void OnTriggerExit(Collider collider)
    {
        IWieldable collidedItem = collider.GetComponent<IWieldable>();
        if (collidedItem)
        {
            objectsHoveringOver.Remove(collidedItem);
        }
    }

}

