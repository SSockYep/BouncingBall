using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : MonoBehaviour
{

    public Rigidbody rb;

    public float acceleration = 10.0f;
    public float deacceleration = 0.3f;
    public float maxSpeed = 4.0f;
    public float bounceSpeed = 8.0f;

    public float gravityScale = 2.0f;

    public static float globalGravity = -9.81f;

    float deaccelerationX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float speed = rb.velocity.x;
        if (!GameObject.Find("StageManager").GetComponent<StageManager>().GetIsPaused())
        {
            if (Mathf.Abs(movementX) >= 0.1f && speed < maxSpeed)
            {
                rb.AddForce(new Vector3(1.0f
                    , 0.0f, 0.0f) * acceleration);
            }
            if (Mathf.Abs(speed) >= maxSpeed)
            {
                speed *= Mathf.Abs(maxSpeed / speed);
                rb.velocity = new Vector3(speed, rb.velocity.y, 0.0f);
            }
            if (speed > 0.0f && movementX < 0.1f)
            {
                rb.velocity = new Vector3(
                    Mathf.SmoothDamp(rb.velocity.x, 0, ref deaccelerationX, deacceleration),
                    rb.velocity.y, 0);
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
        if (c.collider.tag == "Bouncy")
        {
            rb.velocity = new Vector3(rb.velocity.x, bounceSpeed, rb.velocity.z);
        }
    }
}
