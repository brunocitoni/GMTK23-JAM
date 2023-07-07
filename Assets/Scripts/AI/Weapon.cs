using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public int damage;
    [Tooltip("How close the person moves before they attack")]
    public float range;
    [Tooltip("How far the weapon can swing and still hit")]
    public float weaponLength;
    [Tooltip("The speed the holder moves when performing an attack")]
    public float attackMoveSpeed = 5;
}
