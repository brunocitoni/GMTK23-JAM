using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : AIBase
{
    public override void Searching()
    {
        target = GameObject.FindGameObjectWithTag("Hero");
        if(target == null || target.GetComponent<Health>().hasDied)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        base.Searching();
    }

    public override void Persueing()
    {
        base.Persueing();
    }

    public override void Attacking()
    {
        //Checks if player is still within range and attacks when the conditions are good
        base.Attacking();
    }

    public override void AvoidingDamage()
    {
        base.AvoidingDamage();
    }
}
