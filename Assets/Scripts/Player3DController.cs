using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player3DController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;
    public StageManager sm;

    public float acceleration = 1.0f;
    public float deacceleration = 1.0f;
    public float maxSpeed = 4.0f;
    public float bounceSpeed = 10.0f;

    public float gravityScale = 1.0f;

    public static float globalGravity = -9.81f;


    private float deaccelerationX;
    private float deaccelerationZ;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");
        if (!sm.GetIsPaused())
        {
            Vector3 direction = new Vector3(movementX, 0.0f, movementY).normalized;

            Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.z);
            float speed = Mathf.Sqrt(Vector2.Dot(vel, vel));

            if (direction.magnitude >= 0.1f && speed < maxSpeed)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
                rb.AddForce(moveDir.normalized * acceleration);
            }
            if (speed >= maxSpeed)
            {
                vel *= maxSpeed / speed;
                rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.y);
            }
            if (vel.magnitude > 0.0f && direction.magnitude < 0.1f)
            {
                rb.velocity = new Vector3(
                    Mathf.SmoothDamp(rb.velocity.x, 0, ref deaccelerationX, deacceleration),
                    rb.velocity.y,
                    Mathf.SmoothDamp(rb.velocity.z, 0, ref deaccelerationZ, deacceleration));
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
    void OnCollisionEnter(Collision c)
    {
        if(c.collider.tag == "Bouncy")
        {
            rb.velocity = new Vector3(rb.velocity.x, bounceSpeed, rb.velocity.z);
        }
    }

}

