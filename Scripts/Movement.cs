using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidb;
    private PolygonCollider2D polycoll;
    private SpriteRenderer sprite;
    private Animator anim;
   
    public GameObject attackPoint;
    public float raduis;
    public LayerMask enemies;
    public float damage;

    [SerializeField] private LayerMask jumpableGround;

    private float xdirection = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState {idle, walk, running, jumping, falling, attacking}

    private void Start()
    {
       rigidb= GetComponent<Rigidbody2D>();
        polycoll = GetComponent<PolygonCollider2D>();
        sprite= GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();
    }

    private void Update()
    {
        xdirection = Input.GetAxisRaw("Horizontal");
        rigidb.velocity = new Vector2(xdirection * moveSpeed, rigidb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidb.velocity = new Vector2(rigidb.velocity.x, jumpForce);
        }
        

        UpdateAnimationState();
    }

    private void UpdateAnimationState() 
    {
        MovementState state;

        if (xdirection > 0f)
        {
            state = MovementState.running;
            sprite.flipX= false;
        }
        else if (xdirection < 0f) 
        {
            state = MovementState.running;
            sprite.flipX= true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rigidb.velocity.y > .1f) 
        {
            state = MovementState.jumping;
        }
        else if (rigidb.velocity.y < -.1f) 
        {
            state = MovementState.falling;
        }
        if (Input.GetButtonDown("Fire1")) 
        {
            anim.SetBool("isAttacking", true);
        }

        anim.SetInteger("state", (int) state);
    }

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(polycoll.bounds.center, polycoll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

    }

    public void endAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    public void Attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, raduis, enemies);

        foreach(Collider2D enemyGameobject in enemy)
        {
            Debug.Log("Hit Enemy");
            enemyGameobject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, raduis); 
    }
}
