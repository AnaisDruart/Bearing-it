using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeroMove : MonoBehaviour
{
    public float speed;
    public int forceConst = 2;

    private bool canJump;
    private Rigidbody selfRigidbody;
    public float WalkSpeed = 5;
    public float RunSpeed = 6;

    public float gravity = -12;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    Transform cameraT;
    CharacterController controller;

    public bool canMove;

    void Start()
    {
        speed = 10f;
        selfRigidbody = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        
        transform.Translate(speed * Input.GetAxis("Horizontal")/2 * (Time.deltaTime*1/2), 0f, speed * Input.GetAxis("Vertical")/2 * (Time.deltaTime*1/2)); // pour bouger de gauche à droite       
        if (canJump) // pour sauter
        {
            canJump = false;
            
            selfRigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (selfRigidbody.transform.position.y < 0.3)
            {
                canJump = true;
            }
        }

        if (!canMove)
        {
            return;
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        }


        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? RunSpeed : WalkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;
        //transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        if (controller.isGrounded)
        {
            velocityY = 0;
        }
    }

 
}