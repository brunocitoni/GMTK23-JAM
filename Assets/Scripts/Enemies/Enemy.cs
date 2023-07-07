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
    }

    private void Die()
    {
        // delete this and drop one of the possible items from enemy drop list TODO
    }

    //debug only
    private void Update()
    {
        transform.Translate(Vector3.left * thisEnemy.moveSpeed*100 * Time.deltaTime);
    }

}
