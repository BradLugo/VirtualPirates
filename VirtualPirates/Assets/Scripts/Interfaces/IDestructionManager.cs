using UnityEngine;
using System.Collections;

public interface IDestructionManager {
    bool isDestructable { get; set; }

    void Destroy();
}
