using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    PlayerCameraController cameraController;
    Rigidbody rb;
    public Transform playerModel;
    public Animator animator;
    public GameObject princeMesh;
    public GameObject princessMesh;
    public GameObject playerLight;

    public float acceleration;
    public float friction;
    public float maxSpeed;
    Vector3 velocity;

    LayerMask raycastMask = 1 << 8;
    public float groundedCheckAbove;
    public float groundedCheckDown;
    public float gravity;
    public float maxFallSpeed;
    public float flipVelocity;
    public float dotProductForGrounded;

    SphereCollider boxCollider;
    bool canFlip = true;
    public float flipCheckRadius;


    Vector3 respawnPoint;


    // Use this for initialization
    void Start()
    {
        respawnPoint = this.transform.position;
        rb = this.gameObject.GetComponent<Rigidbody>();
        boxCollider = this.gameObject.GetComponent<SphereCollider>();
        cameraController = this.gameObject.GetComponent<PlayerCameraController>();


    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        HandleGroundMovement();

        //if (!CheckAboveGround())
        velocity += GameManager.instance.flip * Vector3.down * gravity * Time.deltaTime;

        velocity = new Vector3(velocity.x, Mathf.Clamp(velocity.y, -maxFallSpeed, maxFallSpeed), velocity.z);

        if (GameManager.instance.flip > 0 && this.transform.position.y > 500)
        {
            this.transform.position += Vector3.down * 1000f;
        }

        if (GameManager.instance.flip < 0 && this.transform.position.y < 500)
        {
            this.transform.position += Vector3.up * 1000f;
        }


        if (Input.GetKeyDown(KeyCode.Space) && CheckAboveGround() && canFlip)
        {
            GameManager.instance.TryFlip();
        }


        playerModel.localScale = new Vector3(1, GameManager.instance.flip, 1);
        bool isMoving = GetGroundedVelocity().magnitude > .03f;
        animator.SetBool("Moving", isMoving);

        if (GameManager.instance.flip > 0)
        {
            princessMesh.SetActive(false);
            princeMesh.SetActive(true);
            playerLight.SetActive(false);
        }
        else
        {
            princessMesh.SetActive(true);
            princeMesh.SetActive(false);
            playerLight.SetActive(true);
        }
        if (isMoving)
            playerModel.rotation = Quaternion.Euler(0, 90 + Mathf.Rad2Deg * Mathf.Atan2(-velocity.z, velocity.x), 0);


    }

    void HandleGroundMovement()
    {
        Vector3 adjustedInput = InCameraDirection(GetInputDirection());
        Vector3 groundMovement = adjustedInput;
        if (groundMovement.magnitude != 0)
        {
            //if (moveState == MoveState.grounded)
            groundMovement = groundMovement * acceleration * Time.deltaTime;
            //else
            //    groundMovement = groundMovement * airAcceleration * Time.deltaTime;
            velocity += groundMovement;

            // Add friction
        }
        else
        {
            //TODO Super friction if going off edge
            Vector3 groundVector = GetGroundedVelocity();
            float frictionForce;
            //if (moveState == MoveState.grounded)
            frictionForce = Mathf.Min(groundVector.magnitude, friction * Time.deltaTime);
            //else
            //    frictionForce = Mathf.Min(groundVector.magnitude, airFriction * Time.deltaTime);
            velocity -= groundVector.normalized * frictionForce;

        }

        // Limit the max speed of the player
        float groundSpeed = GetGroundedVelocity().magnitude;
        if (groundSpeed > maxSpeed)
            velocity = new Vector3(velocity.x / groundSpeed * maxSpeed, velocity.y, velocity.z / groundSpeed * maxSpeed);

        if (GameManager.instance.IsFlipping())
        {
            if (GameManager.instance.flipState == FlipState.goingDown)
                transform.position += Vector3.down * flipVelocity * Time.deltaTime;
            else
                transform.position += Vector3.up * flipVelocity * Time.deltaTime;
        }
    }

    public void FixedUpdate()
    {
        boxCollider.center = new Vector3(0, .5f * GameManager.instance.flip, 0);
        if (GameManager.instance.IsFlipping())
        {
            if (GameManager.instance.flipState == FlipState.goingDown)
                transform.position += Vector3.down * flipVelocity * Time.deltaTime;
            else
                transform.position += Vector3.up * flipVelocity * Time.deltaTime;
        }
        else
        {
            this.transform.position += velocity;
            if (this.transform.position.y < -15 || this.transform.position.y > 1025)
            {
                this.transform.position = respawnPoint;
                GameManager.instance.ResetFlip();
            }
        }
        canFlip = true;
    }

    bool CheckAboveGround()
    {
        for (int xIndex = -1; xIndex <= 1; xIndex += 2)
        {
            for (int zIndex = -1; zIndex <= 1; zIndex += 2)
            {
                if (!Physics.Raycast(transform.position + GameManager.instance.flip * Vector3.up * groundedCheckAbove + new Vector3(xIndex, 0, zIndex) * flipCheckRadius, Vector3.down * GameManager.instance.flip, groundedCheckDown, raycastMask))
                    return false;
            }
        }


        if (Physics.Raycast(transform.position + GameManager.instance.flip * Vector3.up * groundedCheckAbove, Vector3.down * GameManager.instance.flip, groundedCheckDown, raycastMask))
        {
            return true;
        }
        return false;

    }

    Vector3 GetInputDirection()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    public Vector3 InCameraDirection(Vector3 inVec)
    {
        float rotTemp = (0 - cameraController.GetRotation().y) * Mathf.Deg2Rad;
        return new Vector3(inVec.x * Mathf.Cos(rotTemp) - inVec.z * Mathf.Sin(rotTemp), 0, inVec.z * Mathf.Cos(rotTemp) + inVec.x * Mathf.Sin(rotTemp));
    }

    public Vector3 GetGroundedVelocity()
    {
        return new Vector3(velocity.x, 0, velocity.z);
    }

    public void OnTriggerStay(Collider c)
    {
        if (c.tag == "Red")
        {
            canFlip = false;
        }
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.layer == 8)
        {
            foreach (ContactPoint contact in c.contacts)
            {
                //float dotProduct = Vector3.Dot((contact.point - transform.position).normalized, Vector3.down);
                float dotProduct = Vector3.Dot(transform.TransformDirection(Vector3.up), contact.normal);
                if (Mathf.Abs(dotProduct) >= dotProductForGrounded)
                {
                    //Debug.DrawLine(contact.point, transform.position);
                    this.velocity.y = 0;

                }
            }
        }
    }



}