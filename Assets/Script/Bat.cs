using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    protected override void AttackSystem()
    {
        throw new System.NotImplementedException();
    }

    protected override void PlayerChase()
    {
        base.PlayerChase();
    }

    protected override void StopChasing()
    {
        base.StopChasing();
    }

    
}
