using UnityEngine;
using System.Collections;

public interface IAttackManager {
    int attackValue { get; set; }
    double attackRangeSqr { get; set; }

    void attack(IDefenseManager target);
}
