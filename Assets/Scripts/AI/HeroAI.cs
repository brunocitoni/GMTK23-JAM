using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : AIBase
{
    public override void Searching()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        base.Searching();
    }

    public override void Persueing()
    {
        base.Persueing();
    }

    public override void Attacking()
    {
        base.Attacking();
    }

    public override void AvoidingDamage()
    {
        base.AvoidingDamage();
    }
}
