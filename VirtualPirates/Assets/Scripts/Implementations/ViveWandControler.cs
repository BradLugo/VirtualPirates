using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedController))]
class ViveWandControler : MonoBehaviour
{
    Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    SteamVR_Controller.Device controllerDevice { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    SteamVR_TrackedObject trackedObj;

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
        Debug.Log("Pad Clicked! X: " + e.padX + " " + e.padY);
    }

    void OnUnclickTrigger(object sender, ClickedEventArgs e)
    {
        Debug.Log("Unclicked trigger!");
    }

    void OnClickTrigger(object sender, ClickedEventArgs e)
    {
        Debug.Log("Clicked trigger!");
    }
}
