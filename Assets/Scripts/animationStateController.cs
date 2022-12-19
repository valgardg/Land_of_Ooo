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

    public float walkignSpeed;
    public float runningSpeed;
    public float turningSpeed;
    
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    
    public float groundDrag;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");

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

        // handle left and right turns
        if(leftPressed){
            gameObject.transform.Rotate(0f,-turningSpeed * Time.deltaTime,0f, Space.World);
        }
        if(rightPressed){
            gameObject.transform.Rotate(0f,turningSpeed * Time.deltaTime,0f, Space.World);
        }

        MyInput();
        SpeedControl();

        rb.drag = groundDrag;
    }

     private void FixedUpdate(){
        MovePlayer();
    }

    private void MyInput(){
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");

        verticalInput = 0f;
        if(Input.GetKey("w")){
            verticalInput += 1f;
        }
        if(Input.GetKey("s")){
            verticalInput -= 1f;
        }
    }

    private void MovePlayer(){
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput; //+ orientation.right * horizontalInput;
        // apply the movement
        //print($"move direction: {moveDirection}");
        float speed = animator.GetBool(isRunningHash) ? runningSpeed : walkignSpeed;
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > walkignSpeed){
            Vector3 limitedVel = flatVel.normalized * walkignSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
