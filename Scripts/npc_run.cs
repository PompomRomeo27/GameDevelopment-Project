using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_run : StateMachineBehaviour
{
    public float speed = 5f;
    public float attackRange = 3f;

    Transform maincharac;
    Rigidbody2D rb;

    Npc enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        maincharac = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();

        enemy = animator.GetComponent<Npc>();
    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer();
        

        Vector2 target = new Vector2(maincharac.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(maincharac.position, rb.position) <= attackRange) 
        {
            animator.SetTrigger("attacking");
        }
    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attacking");
    
    }

   
}
