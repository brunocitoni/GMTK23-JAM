using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemySO thisEnemy;

    private EnemyAI ai;
    private Health healthScript;

    // Start is called before the first frame update
    void Start()
    {
        name = thisEnemy.enemyName;
        this.GetComponent<Image>().sprite = thisEnemy.enemySprite;

        // assign a health script to this enemy
        healthScript = this.gameObject.AddComponent<Health>();
        healthScript.SetHealth(thisEnemy.enemyMaxHealth);
        healthScript.OnThisDeath += Die;

        ai = this.gameObject.AddComponent<EnemyAI>();
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
    }

    //debug only
    /*private void Update()
    {
        transform.Translate(Vector3.left * thisEnemy.moveSpeed*100 * Time.deltaTime);
    }*/

}
