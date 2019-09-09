using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    [SerializeField, Tooltip("Speed at which character mooves")] private float movementSpeed = 5f;
    [SerializeField, Tooltip("Force of the players jump")] private float jumpForce = 5f;
    [SerializeField, Tooltip("Position of missile spawn")] private Transform shotSpawn;
    private Vector3 direction;
    private enum CharacterStates {freeRoam, jumping, falling};
    private CharacterStates currentState;
    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        currentState = CharacterStates.freeRoam;
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        setCurrentState();

        Debug.Log(currentState);

        switch (currentState)
        {
            case CharacterStates.freeRoam:
                if (Input.GetKey("w"))
                    moveForward();

                if (Input.GetKey("a"))
                    moveLeft();

                if (Input.GetKey("s"))
                    moveBackwards();

                if (Input.GetKey("d"))
                    moveRight();

                if (Input.GetKeyDown(KeyCode.Space))
                    jump();

                if (Input.GetMouseButtonDown(0))
                    shoot();
                break;

            case CharacterStates.jumping:
                break;

            case CharacterStates.falling:
                break;

            default:
                Debug.Log("bruh i dunno chief");
                break;
        }
	}

    public void moveForward()
    {
        direction = transform.forward;

        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    public void moveLeft()
    {
        direction = -transform.right;

        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    public void moveBackwards()
    {
        direction = -transform.forward;

        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    public void moveRight()
    {
        direction = transform.right;

        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    public void jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void shoot()
    {
        Debug.Log("hello");

        Instantiate(Resources.Load("Cylinder"), shotSpawn.position, shotSpawn.rotation);
    }

    public void setCurrentState()
    {
        if (rb.velocity.y < 0)
            currentState = CharacterStates.falling;

        else if(rb.velocity.y > 0)
            currentState = CharacterStates.jumping;

        else if(rb.velocity.y == 0)
        {
            RaycastHit info;

           

            if (Physics.Raycast(transform.position, -transform.up, out info, 1))
                currentState = CharacterStates.freeRoam;
            else
                currentState = CharacterStates.falling;
        }
    }
}
