using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : AIBase
{
    public override void Searching()
    {
        target = GameObject.Find("Hero");
        base.Searching();
    }

    public override void Persueing()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * movespeed);

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
