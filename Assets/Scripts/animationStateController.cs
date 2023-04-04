using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class animationStateController : MonoBehaviour
{
    public Transform orientation;

    Animator animator;
    Rigidbody rb;

    public bool hasWeapon;

    int isWalkingHash;
    int isRunningHash;
    int isDancingHash;
    int isAttackingDownwardHash;
    int isAttackingHorizontalHash;
    int isAttackingBackhandHash;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    
    public float groundDrag;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isDancingHash = Animator.StringToHash("isDancing");
        isAttackingDownwardHash = Animator.StringToHash("isAttackingDownward");
        isAttackingBackhandHash = Animator.StringToHash("isAttackingBackhand");
        isAttackingHorizontalHash = Animator.StringToHash("isAttackingHorizontal");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        bool dancePressed = Input.GetKey("k");
        animator.SetBool(isDancingHash, false);

        // resetting so it doesnt loop infinitely
        animator.SetBool(isAttackingDownwardHash, false);
        animator.SetBool(isAttackingBackhandHash, false);
        animator.SetBool(isAttackingHorizontalHash, false);

        // if player presses w key
        if(!isWalking && forwardPressed){
            animator.SetBool(isWalkingHash, true);
        }
        // if player is not pressing w key
        if(isWalking && !forwardPressed){
            animator.SetBool(isWalkingHash, false);
        }
        // if player is wakling and presses left shift
        if(!isRunning && (forwardPressed && runPressed)){
            animator.SetBool(isRunningHash, true);
        }
        // if player stops running or stops walking
        if(isRunning && (!runPressed || !forwardPressed)){
            animator.SetBool(isRunningHash, false);
        }

        if(dancePressed){
            animator.SetBool(isDancingHash, true);
        }

        if(!hasWeapon){
            return;
        }

        bool attackPressed = Input.GetMouseButton(0);
        bool heavyAttackPressed = Input.GetKey("left shift");

        if(attackPressed){
            if(heavyAttackPressed){
                animator.SetBool(isAttackingBackhandHash, true);
            }else{
                List<int> attacks = new () {isAttackingHorizontalHash, isAttackingDownwardHash};
                System.Random rnd = new System.Random();
                int attackGenerated = rnd.Next(0, 2);
                animator.SetBool(attacks[attackGenerated], true);
            }
        }

    }
}
