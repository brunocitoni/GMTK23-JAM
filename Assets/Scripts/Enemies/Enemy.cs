using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemySO thisEnemy;

    private EnemyAI ai;
    private Health healthScript;
    ItemSpawner itemSpawner;
    EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        name = thisEnemy.enemyName;
        this.GetComponent<SpriteRenderer>().sprite = thisEnemy.enemySprite;

        // get a reference to the itemSpawner
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();

        // get a reference to the enemySpawner to update the quantity of enemies currently spawned on death
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        // assign a health script to this enemy
        healthScript = this.GetComponent<Health>();
        healthScript.SetHealth(thisEnemy.enemyMaxHealth);
        healthScript.OnThisDeath += Die;

        ai = this.GetComponent<EnemyAI>();
        ai.weaponRangeTolerance = thisEnemy.weaponRangeTolerance;
        ai.movespeed = thisEnemy.moveSpeed;
        ai.attackduration = thisEnemy.attackduration;
        ai.attackCooldown = thisEnemy.attackCooldown;
        ai.attackStopDistance = thisEnemy.attackStopDistance;
        ai.personalSpace = thisEnemy.personalSpace;
    }

    private void Die()
    {
        //Debug.Log("Enemy named " + thisEnemy.enemyName + " just died");
        Debug.Log("Dead");
        // drop item
        for(int i=0; i<thisEnemy.drops.Count; i++)
        {
            float chance =thisEnemy.dropRate[i];
            float prob = Random.Range(0f, 1f);
            Debug.Log(prob);
            if (prob>chance)
            {
                 itemSpawner.InstantiateItem(thisEnemy.drops[i], this.transform);
            }
        }

        //Debug.Log("Destroyinh enemy " + thisEnemy.enemyName);
        // delete this gameobject
        Destroy(this.gameObject);

        // remove this enemy from currently spawned ones
        enemySpawner.currentNumberOfEnemiesSpawned--;
        EnemySpawner.enemyKilled++;

        //Debug.Log("enemies left: " + enemySpawner.currentNumberOfEnemiesSpawned);

        // check if all the enemies are dead while the wave timer is over
        if (enemySpawner.currentNumberOfEnemiesSpawned <= 0 && !WaveManager.isWaveOngoing)
        {
            WaveManager.InvokeWaveComplete();
        }
    }

    public void OnDestroy()
    {
        healthScript.OnThisDeath -= Die;
    }

}
