using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAI : AIBase
{
    //  [Header("Hero Specific")]

    public override void Start()
    {
        base.Start();

        WaveManager.OnWaveComplete += ExitBattle;
        WaveManager.OnWaveStart += StartBattle;
    }

    public void OnDestroy()
    {
        WaveManager.OnWaveComplete -= ExitBattle;
        WaveManager.OnWaveStart -= StartBattle;
    }

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
                if (enemy.GetComponent<Health>().hasDied) continue;

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

    public override void OutOfBattle()
    {
        base.OutOfBattle();
        targetPosition = Vector2.zero;

        moveDirection = (targetPosition - (Vector2)transform.position).normalized;
    }

    public override void Dead()
    {
        if(GetComponent<Health>().currentHealth > 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyAI>().ResetState();
            }
            currentState = AIStates.searching;
            StartSearch.Invoke();
        }
        base.Dead();
    }

    public void ExitBattle()
    {
        currentState = AIStates.out_of_battle;
    }

    public void StartBattle()
    {
        currentState = AIStates.searching;
        StartSearch.Invoke();
    }

}
