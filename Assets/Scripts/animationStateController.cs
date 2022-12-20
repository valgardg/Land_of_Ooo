using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    public Transform orientation;

    Animator animator;
    Rigidbody rb;

    int isWalkingHash;
    int isRunningHash;
    int isDancingHash;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        bool dancePressed = Input.GetKey("k");

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

    }
}
