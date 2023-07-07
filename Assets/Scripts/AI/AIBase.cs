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
    public float movespeed = 5;

    [Header("Atacking")]
    public float attackduration;
    [Tooltip("The time it takes for this person to be ready to attack again in seconds")]
    public float attackCooldown = 1;
    float attackTimer; //Time left till attack is ready
    [Tooltip("The distance from the enemy at which the person stops and attacks")]
    public float attackStopDistance = 0.5f;
    bool attacking = false;


    [HideInInspector]
    public UnityEvent StartSearch, StartPersue, StartAttack, StartAvoiding;

    void Start()
    {
        currentState = AIStates.searching;

        StartAttack.AddListener(() => attackTimer = attackCooldown);
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
        if (!attacking)
        {
            //Cancel Attacking State if target moves to far away
            if (Vector2.Distance(transform.position, target.transform.position) > heldWeapon.range)
            {
                currentState = AIStates.persueing;
                StartPersue.Invoke();
                return;
            }

            //Start Attack
            if(attackTimer <= 0 && !target.GetComponent<AIBase>().attacking)
            {
                StartCoroutine(Attack());
            }
            attackTimer -= Time.deltaTime;
            return;
        }

        
    }

    IEnumerator Attack()
    {
        attacking = true;

        Vector2 startposition = transform.position;

        //Move towards enemy
        while (Vector2.Distance(transform.position, target.transform.position) > attackStopDistance && Vector2.Distance(transform.position, startposition) < heldWeapon.range)
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, heldWeapon.attackMoveSpeed * Time.deltaTime);
            yield return null;
        }

        //Attack
        if (Vector2.Distance(transform.position, target.transform.position) < heldWeapon.weaponLength)
        {
            Health h = target.GetComponent<Health>();
            if(h != null)
            {
                h.ModifyHealth(-heldWeapon.damage);
            }
            else
            {
                Debug.LogError("Warning... " + target.name + " doesn't have a health script!", target);
            }
        }
        else
        {
            //Missed attack (maybe some sort of effect)
        }

        yield return new WaitForSeconds(attackduration);

        //Move away again
        while (Vector2.Distance(transform.position, startposition) > .05f)
        {
            transform.position = Vector2.Lerp(transform.position, startposition, heldWeapon.attackMoveSpeed * Time.deltaTime);
            yield return null;
        }

        attacking = false;
        attackTimer = attackCooldown;
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
