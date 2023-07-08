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

    // Start is called before the first frame update
    void Start()
    {
        name = thisEnemy.enemyName;
        this.GetComponent<SpriteRenderer>().sprite = thisEnemy.enemySprite;

        // get a reference to the itemSpawner
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();

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
        // delete this and drop one of the possible items from enemy drop list TODO
        Debug.Log("Enemy named " + thisEnemy.enemyName + " just died");

        // drop item
        itemSpawner.InstantiateItem(thisEnemy.drops[Random.Range(0, thisEnemy.drops.Count)], this.transform);

        Debug.Log("Destroyinh enemy " + thisEnemy.enemyName);
        // delete this gameobject
        Destroy(this.gameObject);
    }

}
