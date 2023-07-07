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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerspeed = walkspeed;    
    }

    // Update is called once per frame
    void Update()
    {
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
        
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirection.x*playerspeed,playerDirection.y*playerspeed);
    }
}

