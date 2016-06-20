﻿using UnityEngine;
using System.Collections;
using System;

public class ViveInputManager : MonoBehaviour, IInputManager {

    public bool attackButtonPressed { get { return attackButtonPressed; } }
    public bool jumpButtonPressed { get { return jumpButtonPressed; } }
    public bool moveButtonPressed { get { return moveButtonPressed; } }
    public bool selectButtonPressed { get { return selectButtonPressed; }  }

    public float moveAngle { get { return moveAngle; } }

    public TrapPlacement selectedTrap { get { return selectedTrap; } }
    public Vector3 targetedPoint { get { return targetedPoint; } }
    public IMenuItem menuHoveringOver { get { return menuHoveringOver; } }

    private ViveWandControler rightController;
    private ViveWandControler leftController;

    // Use this for initialization
    void Start () {
        rightController = new ViveWandControler();
        leftController = new ViveWandControler(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
