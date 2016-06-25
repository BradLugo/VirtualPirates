using UnityEngine;
using System.Collections;

public abstract class IWieldable : MonoBehaviour {

    int attackValue { get; set; }

    // TODO this abstract class is not built using best practice, think about refactoring it
    public abstract void BeginInteraction(ViveRightController wand);

    public abstract void EndInteraction(ViveRightController wand);

    public abstract bool IsInteracting();
}
