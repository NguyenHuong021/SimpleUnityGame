using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public LayerMask jumpableGround;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anima;
    private float dirX;
    private CapsuleCollider2D coll;
    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling
    }

    [SerializeField] private AudioSource jumpSoundEffect;
    
  

    [SerializeField] public float moveSpeed = 7f;
    [SerializeField] public float jumpHeight = 18f;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        FixedUpdate();

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }



    private void UpdateAnimationState()
    {
        
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y>.1f)
        {
            state = MovementState.jumping;
        }    
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anima.SetInteger("state", (int)state);

    }
    
    private bool IsGrounded()
    {

        return Physics2D.CapsuleCast(coll.bounds.center, coll.bounds.size,CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.1f, jumpableGround);
    }    
}
