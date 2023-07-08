using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefault", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public int enemyAtk;
    public int enemyMaxHealth;
    public List<ItemSO> drops;

    [Space(10) ,Header("EnemyAI")]
    [Header("Persueing"), Tooltip("This multiplied with the weaponrange is where the person actually stops")]
    public float weaponRangeTolerance = 0.6f;
    public float moveSpeed = 3;
    public float alliesEvasionRadius = 3;

    [Header("Atacking")]
    public float attackduration = 0.2f;
    [Tooltip("The time it takes for this person to be ready to attack again in seconds")]
    public float attackCooldown = 1;
    [Tooltip("The distance from the enemy at which the person stops and attacks")]
    public float attackStopDistance = 0.3f;
    [Tooltip("The distance the person moves back when the attack started too close")]
    public float personalSpace = 1;
}
