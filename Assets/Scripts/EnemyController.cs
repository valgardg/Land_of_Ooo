using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float cooldownTime = 1.0f; // Cooldown time in seconds
    private bool isCooldown = false;

    public float knockbackForce = 10.0f; // The force of the knockback

    public Animator animator;
    public Animator enemyAnimator;

    int isWalkingHash;
    int isDyingHash;

    public GameObject thePlayer;

    public float followSpeed = 3f;    

    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;

        
        isWalkingHash = Animator.StringToHash("isWalking");
        isDyingHash = Animator.StringToHash("isDying");

    }

    public void FollowPlayer(GameObject targetObject, float followDistance, float followSpeed)
{
    Vector3 targetPosition = targetObject.transform.position;
    Vector3 targetPositionWithoutY = Vector3.ProjectOnPlane(targetPosition, Vector3.up);

    Vector3 currentPosition = transform.position;
    Vector3 currentPositionWithoutY = Vector3.ProjectOnPlane(currentPosition, Vector3.up);

    float distance = Vector3.Distance(currentPositionWithoutY, targetPositionWithoutY);

    if (distance <= followDistance)
    {
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, followSpeed * Time.deltaTime);

        Vector3 targetDirection = targetPositionWithoutY - currentPositionWithoutY;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
    }
}

    // Update is called once per frame
    void Update()
    {
        if(health<1){
            Invoke("DestroyOnDeath", 4f);
            enemyAnimator.SetBool(isWalkingHash, false);
            enemyAnimator.SetBool(isDyingHash, true);
        }else{
            //if(Vector3.Distance(transform.position, thePlayer.transform.position) < 60f){
            if(true){
                // set walking
                // move the enemy towards player
                FollowPlayer(thePlayer, 60f, 2.5f);
                enemyAnimator.SetBool(isWalkingHash, true);
            }else{
                // set idle
                enemyAnimator.SetBool(isWalkingHash, false);
            }
        }
        
    }

    public void DestroyOnDeath(){
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword" && (animator.GetCurrentAnimatorStateInfo(0).IsName("FinnStandingMeeleAttackDownward") || animator.GetCurrentAnimatorStateInfo(0).IsName("FinnStandingMeleeAttackBackhand") || animator.GetCurrentAnimatorStateInfo(0).IsName("FinnStandingMeleeAttackHorizontal"))){
            // Calculate the knockback direction
            Vector3 knockbackDirection = transform.position - other.transform.position;
            knockbackDirection = knockbackDirection.normalized;

            // Apply the knockback force
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int damage){
        if(!isCooldown){
            isCooldown = true;
            Invoke("ResetCooldown", cooldownTime);

            health -= damage;
        }
    }

    void ResetCooldown(){
        isCooldown = false;
    }

}
