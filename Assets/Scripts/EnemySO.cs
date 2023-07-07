using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefault", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public int enemyAtk;
    public int enemyHealth;
    public List<ItemSO> drops;
}
