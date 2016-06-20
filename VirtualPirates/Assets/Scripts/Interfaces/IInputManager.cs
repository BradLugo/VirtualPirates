using UnityEngine;

public interface IInputManager
{
    bool jumpButtonPressed { get; }
    bool moveButtonPressed { get; }
    bool selectButtonPressed { get; }
    bool attackButtonPressed { get; }

    IMenuItem menuHoveringOver { get; }
    TrapPlacement selectedTrap { get; }
    Vector3 targetedPoint { get; } // TODO There may be a better way to do this without a vector3 object

    float moveAngle { get; }

}