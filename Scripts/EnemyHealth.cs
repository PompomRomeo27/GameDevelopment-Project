using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float currentHealth;
    private Animator anim;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = health;
        
    }

    void Update()
    {
        if (health < currentHealth)
        {
            currentHealth = health;
            anim.SetTrigger("Attacked");
        }
        

        if (health <= 0) 
        {
            anim.SetBool("isDead", true);
            Debug.Log("Enemy is Dead!");
        }
        
    }

}
