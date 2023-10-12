using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatedPlayerController : MonoBehaviour
{
    //Movement Variables
    private float verticalInput;
    public float moveSpeed;
    private float savedMoveSpeed;
    private float savedJumpForce;
    private float savedTurnSpeed;

    private float horizontalInput;
    public float turnSpeed;

    //Jumping Variables
    private Rigidbody rb;
    public float jumpForce;
    public bool isOnGround;

    //Animation Variables
    private Animator animator;

    //Particles
    public ParticleSystem dustCloud;

    // Start is called before the first frame update
    void Start()
    {
        //Get Components
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        dustCloud.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        //Forward and Backward Movement
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);

        //Activate or Deactivate running
        animator.SetFloat("verticalInput", Mathf.Abs(verticalInput));

        //Activate Dust Cloud
        if (verticalInput > 0 && !dustCloud.isPlaying && moveSpeed != 0)
        {
            dustCloud.Play();
        } else if (verticalInput <= 0 || moveSpeed == 0)
        {
            dustCloud.Stop();
        }

        //Rotation
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);

        //Jumping
        if(Input.GetKeyDown(KeyCode.Space)  && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetBool("isOnGround", isOnGround);
        }

        //Shoot
        if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("shoot");
            savedMoveSpeed = moveSpeed;
            savedJumpForce = jumpForce;
            savedTurnSpeed = turnSpeed;
            moveSpeed = 0;
            jumpForce = 0;
            turnSpeed = 0;
        } else if (animator.GetAnimatorTransitionInfo(0).IsName("Un-die -> Idle"))
        {
            moveSpeed = savedMoveSpeed;
            jumpForce = savedJumpForce;
            turnSpeed = savedTurnSpeed;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isOnGround", isOnGround);
        }
    }

}
