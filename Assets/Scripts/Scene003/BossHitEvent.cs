using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitEvent : MonoBehaviour
{
    public BossFSM bossFSM;

    public void PlayerHit()
    {
        bossFSM.AttackAction();
    }
}
