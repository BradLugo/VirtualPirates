using UnityEngine;
using System.Collections;

public interface IMovementManager {

    int moveSpeed { get; set; }
    int jumpHeight { get; set; }

    // Update is called once per frame (required by monobehavior)

    void Move(Vector3 direction);
}
