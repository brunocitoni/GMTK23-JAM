using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : AIBase
{
  //  [Header("Hero Specific")]


    public override void Searching()
    {
        //Select closest enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = float.PositiveInfinity;
        GameObject tempTarget = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if(distance < closestDistance)
            {
                tempTarget = enemy;

                closestDistance = distance;
            }
        }

        //Apply selection if there are any enemies
        if(tempTarget != null)
        {
            target = tempTarget;
        }

        //The check if the target has been set
        base.Searching();
    }

    public override void Persueing()
    {
        base.Persueing();
    }

    public override void Attacking()
    {
        //Checks if enemy is still within range and attacks when the conditions are good
        base.Attacking();
    }


    public override void AvoidingDamage()
    {
        base.AvoidingDamage();
    }
}
