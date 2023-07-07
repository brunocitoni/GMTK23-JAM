using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBase : MonoBehaviour
{
    public AIStates currentState = AIStates.searching;

    public GameObject target;
    public Vector2 targetPosition;

    public Weapon heldWeapon;

    [Header("Persueing"), Tooltip("This multiplied with the weaponrange is where the person actually stops")]
    public float weaponRangeTolerance = 0.75f;

    [HideInInspector]
    public UnityEvent StartSearch, StartPersue, StartAttack, StartAvoiding;

    void Start()
    {
        currentState = AIStates.searching;
    }

    void Update()
    {
        //Simple statemachine that selects what should be updated based on the current state
        switch (currentState)
        {
            case AIStates.searching:
                Searching();
                break;

            case AIStates.persueing:
                Persueing();
                break;

            case AIStates.attacking:
                Attacking();
                break;

            case AIStates.avoiding_damage:
                AvoidingDamage();
                break;
        }
    }

    /// <summary>
    /// Searches for a target. When a target is set flips the state to persueing
    /// </summary>
    public virtual void Searching()
    {
        if(target != null)
        {
            currentState = AIStates.persueing;
            StartPersue.Invoke();
            return;
        }
        

    }

    /// <summary>
    /// Starts moving towards the target. When the target is close enough flips the state to atacking
    /// </summary>
    public virtual void Persueing()
    {
        targetPosition = target.transform.position;

        if(Vector2.Distance(transform.position, target.transform.position) < heldWeapon.range * weaponRangeTolerance)
        {
            currentState = AIStates.attacking;
            StartAttack.Invoke();
            return;
        }
    }

    /// <summary>
    /// Attacks the target and then decides what to do next.
    /// </summary>
    public virtual void Attacking()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > heldWeapon.range)
        {
            currentState = AIStates.persueing;
            StartPersue.Invoke();
            return;
        }
    }

    public virtual void AvoidingDamage()
    {

    }



    public enum AIStates
    {
        searching,
        persueing,
        attacking,
        avoiding_damage
    }
}
