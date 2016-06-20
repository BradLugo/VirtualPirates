using UnityEngine;
using System.Collections;

public interface IConditionManager {


    void addCondition(Condition cond);

    bool hasCondition(Condition cond);
}
