using UnityEngine;
using System.Collections;
using System;

public class TowerPlacement : MonoBehaviour {

    SteamVR_TrackedController controller;
    public GameObject selectedTower;

    Transform reference
    {
        get
        {
            var top = SteamVR_Render.Top();
            return (top != null) ? top.origin : null;
        }
    }

    // Use this for initialization
    void Start () {
        var trackedController = GetComponent<SteamVR_TrackedController>();
        if (trackedController == null)
        {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        }

        trackedController.TriggerClicked += new ClickedEventHandler(DoClick);
    }

    // Event handler for tower placement
    private void DoClick(object sender, ClickedEventArgs e)
    {
        Ray ray = new Ray(this.transform.position, transform.forward);
        if (controller.gripped)
        {
            // Place tower logic
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
            
            if (hitInfo.distance < 10f)
            {
                // TODO Instantiate the new tower object at hitInfo.point with rotation 0
                Instantiate(selectedTower, hitInfo.point, this.transform.rotation);
            }

        }
    }

    // Update is called once per frame
    void Update () {
	

        
	}
}
