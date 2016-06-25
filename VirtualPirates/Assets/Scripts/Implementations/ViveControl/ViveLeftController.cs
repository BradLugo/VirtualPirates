using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedController))]
class ViveLeftController : MonoBehaviour
{
    Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    SteamVR_Controller.Device controllerDevice { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    SteamVR_TrackedObject trackedObj;
    SteamVR_Teleporter teleporter;

    void OnEnable()
    {
        SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();
        SteamVR_Teleporter teleporter = GetComponent<SteamVR_Teleporter>();
        controller.TriggerClicked += OnClickTrigger;
        controller.TriggerUnclicked += OnUnclickTrigger;
        controller.PadClicked += OnPadClicked;
        teleporter.teleportOnClick = true;
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
        Debug.Log("Left Pad Clicked! X: " + e.padX + " " + e.padY);
    }

    void OnUnclickTrigger(object sender, ClickedEventArgs e)
    {
        Debug.Log("Left Unclicked trigger!");
    }

    void OnClickTrigger(object sender, ClickedEventArgs e)
    {
        Debug.Log("Left Clicked trigger!");
    }
}
