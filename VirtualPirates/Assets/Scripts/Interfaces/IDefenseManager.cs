using UnityEngine;
using System.Collections;

public interface IDefenseManager {
    int defenseValue { get; set; }

    void Defend(int attackingValue);

    float getSize();
}
