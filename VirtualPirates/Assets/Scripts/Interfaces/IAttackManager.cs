using UnityEngine;
using System.Collections;

public interface IAttackManager {
    int attackValue { get; set; }

    void attack(IDefenseManager target);
}
