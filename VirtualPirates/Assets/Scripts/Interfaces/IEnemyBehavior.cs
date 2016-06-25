using UnityEngine;
using System.Collections;

public interface IEnemyBehavior {

    void Move();

    IEnumerator Attack();

}
