using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemySO thisEnemy;

    // Start is called before the first frame update
    void Start()
    {
        name = thisEnemy.enemyName;
        this.GetComponent<Image>().sprite = thisEnemy.enemySprite;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * thisEnemy.moveSpeed*100 * Time.deltaTime);
    }

}
