using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideckickMove : MonoBehaviour
{
    [SerializeField]
    private float walkspeed = 8;
    [SerializeField]
    private float runspeed = 15;
    private float playerspeed;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private static float t = 0.0f;
    private float lastDirection = 0;


    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerspeed = walkspeed;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveManager.waitingForNewWave || PlayerInventory.isPassingItem) {
            playerspeed = 0;
            return;
        }

        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(directionX,directionY).normalized;

        if(Input.GetButton("Fire3")){
            //playerspeed = runspeed;
            playerspeed = Mathf.Lerp(walkspeed,runspeed,t);
            t += 2*Time.deltaTime;
            t = Mathf.Clamp(t,0,1);
        }
        else {
            playerspeed = Mathf.Lerp(runspeed,walkspeed,1-t);
            t -= 2*Time.deltaTime;
            t = Mathf.Clamp(t,0,1);
        }

        //Animation
        if (playerDirection.magnitude < 0.5f)
        {
            animator.SetFloat("XMove", 0);
        }
        else if(Mathf.Abs(playerDirection.x) > 0.3f)
        {
            lastDirection = playerDirection.x;
            animator.SetFloat("XMove", playerDirection.x);
        }
        else
        {
            animator.SetFloat("XMove", lastDirection);
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (playerDirection * playerspeed * Time.deltaTime));
    }
}

