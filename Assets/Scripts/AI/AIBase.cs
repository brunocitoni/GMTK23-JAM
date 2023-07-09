using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBase : MonoBehaviour
{
    public AIStates currentState = AIStates.searching;

    public GameObject target;
    public Vector2 targetPosition;
    public Vector2 moveDirection;
    Rigidbody2D rb;

    public Weapon heldWeapon;

    [Header("Persueing"), Tooltip("This multiplied with the weaponrange is where the person actually stops")]
    public float weaponRangeTolerance = 0.75f;
    public float movespeed = 5;
    public float alliesEvasionRadius = 3;

    [Header("Atacking")]
    public float attackduration;
    [Tooltip("The time it takes for this person to be ready to attack again in seconds")]
    public float attackCooldown = 1;
    float attackTimer; //Time left till attack is ready
    [Tooltip("The distance from the enemy at which the person stops and attacks")]
    public float attackStopDistance = 0.5f;
    [Tooltip("The distance the person moves back when the attack started too close")]
    public float personalSpace = 1;
    bool attacking = false;

    [Header("Graphics")]
    public Transform weaponeffect;
    private Animator anim;

    public AudioClip slashSound;
    public AudioClip slashMissSound;


    [HideInInspector]
    public UnityEvent StartSearch, StartPersue, StartAttack, StartAvoiding;

    public virtual void Start()
    {
        currentState = AIStates.searching;

        StartAttack.AddListener(SetUpAttack);

        if(GetComponent<Health>() != null)
            GetComponent<Health>().OnThisDeath += OnDeath;

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    void SetUpAttack()
    {
        attackTimer = attackCooldown;
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

            case AIStates.out_of_battle:
                OutOfBattle();
                break;
                
            case AIStates.dead:
                Dead();
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * movespeed;
    }

    /// <summary>
    /// Searches for a target. When a target is set flips the state to persueing
    /// </summary>
    public virtual void Searching()
    {
        moveDirection = Vector2.zero;

        if (target != null)
        {
            currentState = AIStates.persueing;
            StartPersue.Invoke();
            return;
        }
        else
        {
            currentState = AIStates.searching;
            StartSearch.Invoke();
        }
    }

    /// <summary>
    /// Starts moving towards the target. When the target is close enough flips the state to atacking
    /// </summary>
    public virtual void Persueing()
    {
        targetPosition = target.transform.position;
        moveDirection = (targetPosition - (Vector2)transform.position).normalized;

        Collider2D[] allieCollisions = Physics2D.OverlapCircleAll(transform.position, alliesEvasionRadius);
        int counter = 0;
        Vector2 totalPosition = Vector2.zero;
        foreach(Collider2D collider in allieCollisions)
        {
            if (collider.CompareTag(tag) && collider.gameObject != gameObject)
            {
                totalPosition.x += collider.transform.position.x;
                totalPosition.y += collider.transform.position.y;
                counter++;
            }
        }
        if(counter != 0)
        {
            Vector2 centerOfGroup = new Vector2(totalPosition.x / counter, totalPosition.y / counter);
            Vector2 evationDirection = -(centerOfGroup - (Vector2)transform.position).normalized * 0.5f;
            moveDirection += evationDirection;
            moveDirection.Normalize();
        }

        if(Vector2.Distance(transform.position, target.transform.position) < heldWeapon.range * weaponRangeTolerance)
        {
            currentState = AIStates.attacking;
            StartAttack.Invoke();
            return;
        }

        if (target == null)
        {
            currentState = AIStates.searching;
            StartSearch.Invoke();
        }
    }

    /// <summary>
    /// Attacks the target and then decides what to do next.
    /// </summary>
    public virtual void Attacking()
    {
        moveDirection = Vector2.zero;

        if (target == null)
        {
            currentState = AIStates.searching;
            StartSearch.Invoke();
        }

        if (!attacking)
        {
            //Cancel Attacking State if target moves to far away
            try //Removing an error that is called that doesn't cause any problems
            {
                if (Vector2.Distance(transform.position, target.transform.position) > heldWeapon.range)
                {
                    currentState = AIStates.persueing;
                    StartPersue.Invoke();
                    return;
                }
            }
            catch { }
            

            //Start Attack
            if(attackTimer <= 0)
            {
                StartCoroutine(Attack());
            }
            attackTimer -= Time.deltaTime;
            return;
        }

    }

    bool effectFollowTarget = false;
    IEnumerator Attack()
    {
        float timer = 0;
        attacking = true;
        AIBase targetAI = target.GetComponent<AIBase>();

        Vector2 startposition = transform.position;

        //Move towards enemy
        timer = 0.4f;
        while (timer > 0 && Vector2.Distance(transform.position, target.transform.position) > attackStopDistance && Vector2.Distance(transform.position, startposition) < heldWeapon.range)
        {
            timer -= Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, target.transform.position, heldWeapon.attackMoveSpeed * Time.deltaTime);
            yield return null;

            //If the target died
            if(target == null)
            {
                attacking = false;
                attackTimer = attackCooldown;
                yield break;
            }
        }

        //Attack
        if (Vector2.Distance(transform.position, target.transform.position) < heldWeapon.weaponLength)
        {

            Health h = target.GetComponent<Health>();
            if(h.GetComponent<PlayerHealth>() != null)
            {
                Debug.Log("Attacked the player");
            }
            if(h != null)
            {
                if (h.ModifyHealth(-heldWeapon.damage))
                {
                    target = null;
                    attacking = false;
                    attackTimer = attackCooldown;
                    yield break;
                }

            }
            else
            {
                Debug.LogError("Warning... " + target.name + " doesn't have a health script!", target);
            }

            //Play attack effect
            effectFollowTarget = true;
            weaponeffect.position = target.transform.position;
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            weaponeffect.rotation = q;

            anim.SetTrigger("Slash");

            //CameraShake
            CameraManager.Shake(0.1f, 0.15f);

            //SOund
            AudioManager.PlaySound(slashSound, new Vector2(-.2f, .2f));
        }
        else
        {
            AudioManager.PlaySound(slashMissSound, new Vector2(-.2f, .2f));
        }

        timer = attackduration;
        if (effectFollowTarget)
        {
            while(timer > 0)
            {
                weaponeffect.position = target.transform.position;
                yield return 0;
                timer -= Time.deltaTime;
            }

            effectFollowTarget = false;
        }
        else
        {
            yield return new WaitForSeconds(attackduration);
        }


        //Move away again
        if (Vector2.Distance(startposition, target.transform.position) < personalSpace)
        {

            startposition = (Vector2)transform.position + (startposition - (Vector2)transform.position).normalized * personalSpace;
        }

        timer = 0.75f;
        while (timer > 0 && Vector2.Distance(transform.position, startposition) > .05f)
        {
            timer -= Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, startposition, heldWeapon.attackMoveSpeed * Time.deltaTime);
            yield return null;
        }

        //End attack
        attacking = false;
        attackTimer = attackCooldown;

    }

    void OnDeath()
    {
        currentState = AIStates.dead;
    }

    private void OnDestroy()
    {
        GetComponent<Health>().OnThisDeath -= OnDeath;
    }

    public virtual void AvoidingDamage()
    {

    }

    public virtual void OutOfBattle()
    {

    }

    public virtual void Dead()
    {

    }

    public enum AIStates
    {
        searching,
        persueing,
        attacking,
        avoiding_damage,
        out_of_battle,
        dead
    }
}
