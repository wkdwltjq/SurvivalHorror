using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    public EnemyFSM efsm;

    public void PlayerHid()
    {
        efsm.AttackAction();
    }
}
