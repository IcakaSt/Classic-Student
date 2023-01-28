using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [Header("Player Details")]
    [SerializeField] private float speed = 6.0F;
    [SerializeField] private float gravity = -9.81F;
    [SerializeField] private bool jump;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] float fallMultiplier = 2.5f, lowjumpMultiplier = 2f;
    [SerializeField] Vector3 velocity;
    private Vector3 gravityPull;
    private CharacterController controller;

    [Header("Camera Details")]
    [SerializeField] private float turningTime = 0.1f;
    [SerializeField] private float turningVelocity = 0.1f;
    public Transform camera;

    Animator anim;
    [SerializeField] bool run=false;

    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<Spawn>().SpawnPlayer();
        GameObject Model = this.transform.GetChild(1).gameObject;
        anim = Model.GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        //Move
        float horizontal = Input.GetAxis("Horizontal") ;
        float vertical = Input.GetAxis("Vertical") ;

        gravityPull.y += gravity * Time.deltaTime; //Including the gravity


        if (horizontal != 0 || vertical != 0) //Basic movement
        {
            anim.SetBool("Running", true);
            run = true;

            float targetAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turningVelocity, turningTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 directionMove = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(directionMove.normalized * speed * Time.deltaTime);
        }
        else { anim.SetBool("Running", false); run = false; }

        Jumping();
    }

    void Jumping() //For future updates
    {
        //Jump
        controller.Move(velocity * Time.deltaTime);
        if (jump && controller.isGrounded)
        {
            velocity = Vector3.up * jumpPower;
            jump = false;
        }
        velocity += Vector3.up * gravity * (fallMultiplier - 1) * Time.deltaTime;
        if (velocity.y > 0)
        {
            velocity += Vector3.up * gravity * (lowjumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Jump()
    {
        jump = true;
    }
}
