using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderScript : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" && (animator.GetCurrentAnimatorStateInfo(0).IsName("FinnStandingMeeleAttackDownward") || animator.GetCurrentAnimatorStateInfo(0).IsName("FinnStandingMeleeAttackHorizontal"))) {
            Debug.Log("Collision detected!");
            EnemyController enemyController = other.GetComponent<EnemyController>();
            enemyController.TakeDamage(50);
        }
        if (other.tag == "Enemy" && (animator.GetCurrentAnimatorStateInfo(0).IsName("FinnStandingMeleeAttackBackhand"))) {
            Debug.Log("Collision detected!");
            EnemyController enemyController = other.GetComponent<EnemyController>();
            enemyController.TakeDamage(100);
        }
    }   

}
