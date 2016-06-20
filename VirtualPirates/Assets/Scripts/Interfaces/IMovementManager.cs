using UnityEngine;
using System.Collections;

public interface IMovementManager {

    int moveSpeed { get; set; }
    int jumpHeight { get; set; }

    // Use this for initialization (required by monobehavior)
    void Start();

    // Update is called once per frame (required by monobehavior)
    void Update();

    void Update(IInputManager inputManager);
}
